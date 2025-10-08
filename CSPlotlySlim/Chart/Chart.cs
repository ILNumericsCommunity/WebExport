using System;
using CSPlotlySlim.Chart.Traces;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSPlotlySlim.Chart.JSON;
using System.Text;

namespace CSPlotlySlim.Chart;

public sealed class Chart
{
    private const string PlotlyCdn = "https://cdn.plot.ly/plotly-3.1.1.min.js";
    private const string MathJaxCdn = "https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.9/MathJax.js?config=TeX-AMS-MML_SVG.js";

    private readonly JsonSerializerOptions serializerOptions;

    public Chart(params Trace[] traces)
        : this(new Layout.Layout(), traces)
    {
    }

    public Chart(Layout.Layout layout, params Trace[] traces)
    {
        Layout = layout;
        Traces = new List<Trace>(traces);

        // Configure JSON serialization options
        var namingPolicy = new LowerCaseNamingPolicy();
        serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = namingPolicy,
            DictionaryKeyPolicy = namingPolicy,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            Converters =
            {
                new ColorJsonConverter()
            }
        };
    }

    public List<Trace> Traces { get; }

    public Layout.Layout? Layout { get; set; }

    public string Render(string? divName = null)
    {
        if (String.IsNullOrEmpty(divName))
            divName = GetId("chartDiv-");

        // Return a complete HTML document. RenderPartial will include the necessary JS refs and a guarded initialization script when requested.
        var html = $"<html>\r\n<head>\r\n    <meta charset='utf-8' />\r\n    <title>Chart</title>\r\n</head>\r\n<body>\r\n    {RenderPartial(divName)}\r\n</body>\r\n</html>";

        return html;
    }

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