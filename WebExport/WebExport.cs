using System;
using System.Collections.Generic;
using System.IO;
using CSPlotlySlim.Chart;
using CSPlotlySlim.Chart.Layout;
using ILNumerics.Community.WebExport.Generator;
using ILNumerics.Community.WebExport.Generator.Elements;
using ILNumerics.Drawing;
using Trace = CSPlotlySlim.Chart.Traces.Trace;

namespace ILNumerics.Community.WebExport;

/// <summary>
/// Provides methods to export an ILNumerics <see cref="Scene"/> to an HTML page that uses the Plotly
/// JavaScript Open Source Graphing Library for an interactive web chart (https://plotly.com/javascript/).
/// </summary>
/// <remarks>
/// The exported output is a standalone HTML fragment (string) or file that contains the necessary
/// Plotly plot markup and data. Use <see cref="ExportString(Scene)"/> to get the HTML as a string
/// or <see cref="ExportFile(Scene, string)"/> to write the HTML to file.
/// </remarks>
public static class WebExport
{
    /// <summary>
    /// Exports the given ILNumerics <see cref="Scene"/> to an HTML string.
    /// </summary>
    /// <param name="scene">ILNumerics <see cref="Scene"/> to convert. Must not be <c>null</c>.</param>
    /// <returns>
    /// A string containing the HTML fragment for the Plotly chart. Returns an empty string when the scene
    /// cannot be converted to a chart (no traces) or when chart rendering fails.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="scene"/> is <c>null</c>.</exception>
    public static string ExportString(Scene scene)
    {
        if (scene == null)
            throw new ArgumentNullException(nameof(scene));

        var chart = GetChart(scene);

        return chart?.Render() ?? String.Empty;
    }

    /// <summary>
    /// Exports the given ILNumerics <see cref="Scene"/> to an HTML file on disk.
    /// </summary>
    /// <param name="scene">ILNumerics <see cref="Scene"/> to convert. Must not be <c>null</c>.</param>
    /// <param name="filePath">The path of the file to write the exported HTML to.</param>
    /// <returns>
    /// A file containing the HTML page for the Plotly chart. Returns an empty file when the scene
    /// cannot be converted to a chart (no traces) or when chart rendering fails.   
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="scene"/> is <c>null</c>.</exception>
    public static void ExportFile(Scene scene, string filePath)
    {
        if (scene == null)
            throw new ArgumentNullException(nameof(scene));

        var chart = GetChart(scene);

        File.WriteAllText(filePath, chart?.Render() ?? String.Empty);
    }

    /// <summary>
    /// Converts an ILNumerics <see cref="Scene"/> into a CSPlotlySlim <see cref="Chart"/> instance.
    /// </summary>
    /// <param name="scene">ILNumerics <see cref="Scene"/> to convert. Must not be <c>null</c>.</param>
    /// <returns>
    /// A <see cref="Chart"/> representing the scene, or <c>null</c> if no traces could be produced from the scene.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="scene"/> is <c>null</c>.</exception>
    public static Chart? GetChart(Scene scene)
    {
        if (scene == null)
            throw new ArgumentNullException(nameof(scene));

        var layout = new Layout();
        var traces = new List<Trace>();

        var plotCube = scene.First<PlotCube>();
        if (plotCube != null)
            plotCube.Bind<PlotCubeBinder>(plotCube, layout, traces);

        // Failed to bind anything -> return null
        if (traces.Count == 0)
            return null;

        return new Chart(layout, traces.ToArray());
    }
}
