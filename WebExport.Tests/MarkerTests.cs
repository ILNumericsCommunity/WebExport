using System;
using System.Drawing;
using System.IO;
using CSPlotlySlim.Chart;
using CSPlotlySlim.Chart.Generic.Properties;
using CSPlotlySlim.Chart.Traces;
using CSPlotlySlim.Chart.Traces.Properties;
using ILNumerics;
using ILNumerics.Community.WebExport.Generator;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Xunit;
using Exporter = ILNumerics.Community.WebExport.WebExport;

public partial class ExportTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void HollowMarkersRetainTheirBorderAndPixelSize(bool twoD)
    {
        using var scope = Scope.Enter();
        var line = new LinePlot(new float[,] { { 1, 2 }, { 3, 4 }, { 1, 2 } }, markerStyle: MarkerStyle.Circle);
        line.Marker.Size = 17;
        line.Marker.Border.Color = Color.Red;
        line.Marker.Border.Width = 3;
        using var scene = new Scene { new PlotCube(twoDMode: twoD) { line } };
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        var trace = Assert.Single(chart.Traces);
        var marker = twoD ? Assert.IsType<ScatterTrace>(trace).Marker : Assert.IsType<Scatter3DTrace>(trace).Marker;
        Assert.NotNull(marker);
        Assert.Equal(17, marker.Size);
        Assert.Equal(0, marker.Color!.Value.A);
        Assert.Equal(Color.Red, marker.Line!.Color);
        Assert.Equal(3, marker.Line.Width);

        var path = Environment.GetEnvironmentVariable("WEBEXPORT_MARKER_SMOKE_PATH");
        if (twoD && !String.IsNullOrWhiteSpace(path))
            File.WriteAllText(path, Exporter.ExportString(scene, HtmlRenderMode.Offline));
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(false, false)]
    public void MarkerFillAndBorderVisibilityAreIndependent(bool fill, bool border)
    {
        using var scope = Scope.Enter();
        var line = new LinePlot(new float[,] { { 1, 2 }, { 3, 4 } }, markerStyle: MarkerStyle.Square);
        line.Marker.Fill.Color = Color.FromArgb(128, 10, 20, 30);
        line.Marker.Fill.Visible = fill;
        line.Marker.Border.Color = Color.Blue;
        line.Marker.Border.Width = 2;
        line.Marker.Border.Visible = border;
        using var scene = new Scene { new PlotCube(twoDMode: true) { line } };
        var trace = Assert.IsType<ScatterTrace>(Assert.Single(Exporter.GetChart(scene)!.Traces));
        Assert.Equal(fill || border ? ScatterMode.LinesMarkers : ScatterMode.Lines, trace.Mode);
        Assert.Equal(fill ? 128 : 0, trace.Marker!.Color!.Value.A);
        Assert.Equal(border ? 2 : 0, trace.Marker.Line!.Width);
        Assert.Equal(Color.Blue, trace.Marker.Line.Color);
    }

    [Fact]
    public void ZeroSizeMarkersAreNotMadeVisibleByExport()
    {
        using var scope = Scope.Enter();
        var line = new LinePlot(new float[,] { { 1, 2 }, { 3, 4 } }, markerStyle: MarkerStyle.Dot);
        line.Marker.Size = 0;
        using var scene = new Scene { new PlotCube(twoDMode: true) { line } };
        var trace = Assert.IsType<ScatterTrace>(Assert.Single(Exporter.GetChart(scene)!.Traces));
        Assert.Equal(ScatterMode.Lines, trace.Mode);
    }

    [Theory]
    [InlineData(MarkerStyle.TriangleUp)]
    [InlineData(MarkerStyle.TriangleDown)]
    [InlineData(MarkerStyle.TriangleLeft)]
    [InlineData(MarkerStyle.TriangleRight)]
    public void Unsupported3DMarkerShapesUseAnExplicitFallback(MarkerStyle style)
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube(twoDMode: false) { new LinePlot(new float[,] { { 1, 2 }, { 3, 4 }, { 1, 2 } }, markerStyle: style) } };
        var trace = Assert.IsType<Scatter3DTrace>(Assert.Single(Exporter.GetChart(scene)!.Traces));
        Assert.Equal(Symbol.Circle, trace.Marker!.Symbol);
        Assert.Contains(Exporter.ExportWithReport(scene).Issues, issue => issue.Message.Contains("approximated by a circle"));
    }

    [Fact]
    public void RectangleMarkersDoNotAcquireACross()
    {
        Assert.Equal(Symbol.Square,
                     FormatUtility.ToPlotlyMarkerSymbol(MarkerStyle.Rectangle));
    }
}
