using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using CSPlotlySlim.Chart;
using CSPlotlySlim.Chart.Layout;
using ILNumerics.Community.WebExport.Generator.Elements;
using ILNumerics.Drawing;
using Trace = CSPlotlySlim.Chart.Traces.Trace;

namespace ILNumerics.Community.WebExport;

/// <summary>Exports supported ILNumerics plots to Plotly HTML.</summary>
/// <remarks>Invoke on the scene-owning thread with no concurrent scene mutation. Export creates managed snapshots,
/// leaves the scene unchanged, and does not require a UI or renderer. Dispose the source scene when finished.</remarks>
public static class WebExport
{
    private const string Placeholder =
        "<div class='ilnumerics-export-placeholder' role='note'>HTML export is not available for this scene or plot. No supported visible plot data was found.</div>";

    /// <summary>Exports each plot cube as a separate chart in one HTML document.</summary>
    /// <remarks>
    /// Unsupported or empty plots produce an explicit placeholder. Unexpected conversion and
    /// serialization exceptions propagate. Charts use CDN-hosted Plotly and MathJax libraries.
    /// Plot cubes are emitted in scene order; their original viewport arrangement is not retained.
    /// </remarks>
    /// <param name="scene">Caller-owned scene to snapshot on its owning thread.</param>
    /// <returns>A complete UTF-8-compatible HTML document, including placeholders for unavailable content.</returns>
    /// <exception cref="ArgumentNullException">The scene is null.</exception>
    public static string ExportString(Scene scene) => ExportWithReport(scene).Html;

    /// <summary>Exports with an explicit JavaScript library source.</summary>
    /// <param name="scene">Caller-owned scene to snapshot on its owning thread.</param>
    /// <param name="mode">JavaScript source: CDN, embedded offline bundle, or libraries supplied by the host page.</param>
    /// <returns>A complete UTF-8-compatible HTML document, including placeholders for unavailable content.</returns>
    /// <exception cref="ArgumentNullException">The scene is null.</exception>
    public static string ExportString(Scene scene, HtmlRenderMode mode) => ExportWithReport(scene, mode).Html;

    /// <summary>Exports HTML and machine-readable diagnostics for omitted or approximated content.</summary>
    /// <remarks>HTML diagnostics appear in a collapsed export report after the charts.</remarks>
    /// <param name="scene">Caller-owned scene to snapshot on its owning thread.</param>
    /// <param name="mode">JavaScript source: CDN, embedded offline bundle, or libraries supplied by the host page.</param>
    /// <returns>HTML and an immutable list of conversion limitations. Unexpected failures propagate.</returns>
    /// <exception cref="ArgumentNullException">The scene is null.</exception>
    public static ExportResult ExportWithReport(Scene scene, HtmlRenderMode mode = HtmlRenderMode.Cdn)
    {
        ArgumentNullException.ThrowIfNull(scene);
        if (!Enum.IsDefined(mode))
            throw new ArgumentOutOfRangeException(nameof(mode));
        var content = new StringBuilder();
        var issues = new List<ExportIssue>();
        var index = 0;
        var embedded = false;
        var cubes = new List<PlotCube>();
        CollectCubes(scene, cubes, issues);
        foreach (var cube in cubes)
        {
            var binder = new PlotCubeBinder();
            var chart = CreateChart(cube, binder);
            if (chart != null)
            {
                content.Append(chart.RenderPartial(null, mode == HtmlRenderMode.Offline && embedded ? HtmlRenderMode.Existing : mode));
                embedded = true;
            }
            else
                content.Append(Placeholder);
            if (chart == null && binder.Issues.Count == 0)
                binder.Issues.Add(new ExportIssue(index, nameof(PlotCube), "No supported visible plot data was found."));
            foreach (var warning in binder.Issues)
                issues.Add(warning with { PlotCubeIndex = index });

            index++;
        }

        if (index == 0)
        {
            content.Append(Placeholder);
            issues.Add(new ExportIssue(-1, nameof(Scene), "No plot cubes were found."));
        }

        if (issues.Count > 0)
        {
            content.Append("<details class='ilnumerics-export-report'><summary>Export report (")
                   .Append(issues.Count)
                   .Append(")</summary>");

            foreach (var issue in issues)
            {
                var prefix = issue.PlotCubeIndex < 0 ? String.Empty : $"Plot {issue.PlotCubeIndex + 1}: ";
                content.Append("<p class='ilnumerics-export-warning' role='note'>")
                       .Append(WebUtility.HtmlEncode($"{prefix}{issue.NodeType}: {issue.Message}"))
                       .Append("</p>");
            }

            content.Append("</details>");
        }

        var html = "<!doctype html><html><head><meta charset='utf-8'><title>Scene export</title></head><body>"
                   + content + "</body></html>";
        return new ExportResult(html, issues.AsReadOnly());
    }

    /// <summary>Writes the same UTF-8 HTML document produced by ExportString, including placeholders.</summary>
    /// <param name="scene">Caller-owned scene to snapshot on its owning thread.</param>
    /// <param name="filePath">Destination file path; an existing file is replaced.</param>
    /// <exception cref="ArgumentNullException">The scene is null.</exception>
    public static void ExportFile(Scene scene, string filePath)
    {
        ArgumentNullException.ThrowIfNull(scene);
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        File.WriteAllText(filePath, ExportString(scene));
    }

    /// <summary>Writes HTML with an explicit library source, including self-contained offline output.</summary>
    /// <param name="scene">Caller-owned scene to snapshot on its owning thread.</param>
    /// <param name="filePath">Destination file path; an existing file is replaced.</param>
    /// <param name="mode">JavaScript source: CDN, embedded offline bundle, or libraries supplied by the host page.</param>
    /// <exception cref="ArgumentNullException">The scene is null.</exception>
    public static void ExportFile(Scene scene, string filePath, HtmlRenderMode mode)
    {
        ArgumentNullException.ThrowIfNull(scene);
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        File.WriteAllText(filePath, ExportString(scene, mode));
    }

    /// <summary>Converts the first plot cube to a chart, or returns null when it has no supported visible data.</summary>
    /// <remarks>Use ExportString to export every plot cube or obtain HTML placeholders.</remarks>
    /// <param name="scene">Caller-owned scene to snapshot on its owning thread.</param>
    /// <returns>The first cube as managed chart data, or null when that cube cannot produce visible supported traces.</returns>
    /// <exception cref="ArgumentNullException">The scene is null.</exception>
    public static Chart? GetChart(Scene scene)
    {
        ArgumentNullException.ThrowIfNull(scene);
        var cube = scene.First<PlotCube>();
        return cube == null ? null : CreateChart(cube);
    }

    /// <summary>Collects visible plot cubes in scene order and reports content outside supported plot containers.</summary>
    private static void CollectCubes(Group group, List<PlotCube> cubes, List<ExportIssue> issues)
    {
        if (!group.Visible)
            return;
        foreach (var node in group.Children)
        {
            if (!node.Visible || node is PointLight || (group is Scene scene && ReferenceEquals(node, scene.Screen)))
                continue;
            if (node is PlotCube cube)
                cubes.Add(cube);
            else if (node is Group nested && (nested.GetType() == typeof(Group) || nested is Camera))
                CollectCubes(nested, cubes, issues);
            else
                issues.Add(new ExportIssue(-1, node.GetType().Name, "Content outside plot cubes was omitted."));
        }
    }

    /// <summary>Snapshots one visible plot cube into managed chart data, returning null if no traces can be emitted.</summary>
    private static Chart? CreateChart(PlotCube cube, PlotCubeBinder? binder = null)
    {
        for (Node? node = cube; node != null; node = node.Parent)
        {
            if (!node.Visible)
                return null;
        }

        var layout = new Layout();
        var traces = new List<Trace>();
        (binder ?? new PlotCubeBinder()).Bind(cube, cube, layout, traces);
        return traces.Count == 0 ? null : new Chart(layout, traces.ToArray());
    }
}
