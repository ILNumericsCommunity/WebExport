using System.Drawing;
using System.Text.Json;
using CSPlotlySlim.Chart;
using CSPlotlySlim.Chart.Generic.Properties;
using CSPlotlySlim.Chart.JSON;
using ILNumerics;
using ILNumerics.Community.WebExport.Generator;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Xunit;
using Exporter = ILNumerics.Community.WebExport.WebExport;

public partial class ExportTests
{
    [Fact]
    public void ColorsRoundTripEveryAlphaValue()
    {
        var options = new JsonSerializerOptions { Converters = { new ColorJsonConverter() } };
        for (var alpha = 0; alpha <= 255; alpha++)
        {
            var color = Color.FromArgb(alpha, 17, 29, 43);
            Assert.Equal(color.ToArgb(), JsonSerializer.Deserialize<Color>(JsonSerializer.Serialize(color, options), options).ToArgb());
        }
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Color>("\"rgba(1,2,3,NaN)\"", options));
    }

    [Fact]
    public void HiddenLegendRemainsHiddenInHtml()
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube { new LinePlot(new float[,] { { 1, 2 }, { 3, 4 } }), new Legend("Series") { Visible = false } } };
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        Assert.Equal(false, chart.Layout!.ShowLegend);
        Assert.All(chart.Traces, trace => Assert.Equal(false, trace.ShowLegend));
    }

    [Theory]
    [InlineData(MarkerStyle.Dot, Symbol.Circle)]
    [InlineData(MarkerStyle.Plus, Symbol.Cross)]
    [InlineData(MarkerStyle.Cross, Symbol.X)]
    public void MarkerSymbolsPreserveShape(MarkerStyle native, Symbol expected) => Assert.Equal(expected, native.ToPlotlyMarkerSymbol());
}
