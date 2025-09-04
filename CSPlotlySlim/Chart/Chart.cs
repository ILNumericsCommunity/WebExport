using CSPlotlySlim.Chart.Traces;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSPlotlySlim.Chart.JSON;

namespace CSPlotlySlim.Chart;

public sealed class Chart
{
    public Chart(params Trace[] traces)
        : this(new Layout.Layout(), traces)
    {
    }

    public Chart(Layout.Layout layout, params Trace[] traces)
    {
        Layout = layout;
        Traces = new List<Trace>(traces);
    }

    public List<Trace> Traces { get; }

    public Layout.Layout? Layout { get; set; }

    public string Render(string divName = "chartDiv")
    {
        var namingPolicy = new LowerCaseNamingPolicy();
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = namingPolicy,
            DictionaryKeyPolicy = namingPolicy,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            Converters = {
                new ColorJsonConverter()
            }
        };

        var jsonString = JsonSerializer.Serialize(this, serializeOptions);

        //language=html
        var htmlCdn = $"<html>\r\n<head>\r\n    <script src='https://cdn.plot.ly/plotly-latest.min.js'></script>\r\n    <script src='https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.7/MathJax.js?config=TeX-AMS-MML_SVG.js'></script>\r\n</head>\r\n<body>\r\n    <div id='{divName}'></div>\r\n    <script>\r\n        var chartJson = '{jsonString}';\r\n        var chart = JSON.parse(chartJson);\r\n        Plotly.newPlot('{divName}', chart.traces, chart.layout)\r\n    </script>\r\n</body>\r\n</html>";

        return htmlCdn;
    }
}