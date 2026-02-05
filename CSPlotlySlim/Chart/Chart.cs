using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSPlotlySlim.Chart.JSON;
using CSPlotlySlim.Chart.Traces;

namespace CSPlotlySlim.Chart;

/// <summary>
/// Represents a Plotly chart that can be rendered to HTML.
/// </summary>
/// <remarks>
/// A chart consists of one or more <see cref="Trace" /> objects that define the data series,
/// and an optional <see cref="Layout.Layout" /> that configures the chart appearance.
/// Use <see cref="Render" /> or <see cref="RenderPartial" /> to generate HTML output.
/// </remarks>
public sealed class Chart
{
    private const string MathJaxCdn = "https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.9/MathJax.js?config=TeX-AMS-MML_SVG.js";
    private const string PlotlyCdn = "https://cdn.plot.ly/plotly-3.3.1.min.js";

    private readonly JsonSerializerOptions serializerOptions;

    /// <summary>
    /// Initializes a new chart with the specified traces and default layout.
    /// </summary>
    /// <param name="traces">The data traces to display in the chart.</param>
    public Chart(params Trace[] traces)
        : this(new Layout.Layout(), traces)
    {
    }

    /// <summary>
    /// Initializes a new chart with the specified layout and traces.
    /// </summary>
    /// <param name="layout">The layout configuration for the chart.</param>
    /// <param name="traces">The data traces to display in the chart.</param>
    public Chart(Layout.Layout layout, params Trace[] traces)
    {
        Layout = layout;
        Traces = new List<Trace>(traces);

        // Configure JSON serialization options
        var namingPolicy = new PlotlyNamingPolicy();
        serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = namingPolicy, DictionaryKeyPolicy = namingPolicy, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = false,
            Converters = { new ColorJsonConverter() }
        };
    }

    /// <summary>
    /// Gets or sets the layout configuration for this chart.
    /// </summary>
    public Layout.Layout? Layout { get; set; }

    /// <summary>
    /// Gets the list of traces in this chart.
    /// </summary>
    public List<Trace> Traces { get; }

    /// <summary>
    /// Renders the chart as a complete HTML document.
    /// </summary>
    /// <param name="divName">Optional ID for the chart container div. If not specified, a unique ID is generated.</param>
    /// <returns>A complete HTML document string containing the Plotly chart.</returns>
    public string Render(string? divName = null)
    {
        if (String.IsNullOrEmpty(divName))
            divName = GetId("chartDiv-");

        // Return a complete HTML document. RenderPartial will include the necessary JS refs and a guarded initialization script when requested.
        var html = $"<html>\r\n<head>\r\n    <meta charset='utf-8' />\r\n    <title>Chart</title>\r\n</head>\r\n<body>\r\n    {RenderPartial(divName)}\r\n</body>\r\n</html>";

        return html;
    }

    /// <summary>
    /// Renders the chart as an HTML fragment suitable for embedding in an existing page.
    /// </summary>
    /// <param name="divName">Optional ID for the chart container div. If not specified, a unique ID is generated.</param>
    /// <returns>An HTML fragment containing the chart div and initialization script.</returns>
    /// <remarks>
    /// The generated fragment includes a self-contained script that loads Plotly and MathJax
    /// from CDN if they are not already present on the page. Multiple fragments can be safely
    /// embedded on the same page.
    /// </remarks>
    public string RenderPartial(string? divName = null)
    {
        if (String.IsNullOrEmpty(divName))
            divName = GetId("chartDiv-");

        var jsonString = JsonSerializer.Serialize(this, serializerOptions);

        // Encode the JSON as base64 so we can safely embed it in a single-quoted script without escaping JSON characters.
        var jsonBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));

        // RenderScript includes a dynamic loader that will load Plotly and MathJax only once per page.
        var script = $$"""
                       <script>
                       (function() {
                           window.__ilnCSPlotly__ = window.__ilnCSPlotly__ || {};

                           try {
                               var chart = JSON.parse(atob('{{jsonBase64}}'));
                           } catch (e) {
                               console.error('Failed to parse chart json.', e);
                               return;
                           }

                           function loadScript(src) {
                               return new Promise(function(res, rej) {
                                   var s = document.createElement('script');
                                   s.src = src;
                                   s.onload = res;
                                   s.onerror = rej;
                                   document.head.appendChild(s);
                               });
                           }

                           // Create the loader Promise once; subsequent partials reuse it.
                           if (!window.__ilnCSPlotly__.loader) {
                               if (window.Plotly) {
                                   window.__ilnCSPlotly__.loader = Promise.resolve();
                               } else {
                                   window.__ilnCSPlotly__.loader = loadScript('{{PlotlyCdn}}')
                                         .then(function() { return loadScript('{{MathJaxCdn}}'); });
                               }
                           }

                           // Use the loader to render this chart when libs are ready
                           window.__ilnCSPlotly__.loader.then(function() {
                               try {
                                   Plotly.newPlot('{{divName}}', chart.traces, chart.layout);
                               } catch (e) {
                                   console && console.error && console.error('Failed to render chart.', e);
                               }
                           }).catch(function(e) { console && console.error && console.error(e); });
                       })();
                       </script>
                       """;

        return $"<div id='{divName}'></div>\r\n    {script}";
    }

    #region Private

    private static string GetId(string type) => type + Guid.NewGuid().ToString("N");

    #endregion
}
