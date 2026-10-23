using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using CSPlotlySlim.Chart;
using CSPlotlySlim.Chart.Traces;
using CSPlotlySlim.Chart.Traces.Properties;
using ILNumerics;
using ILNumerics.Community.ExportSamples;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Xunit;
using Exporter = ILNumerics.Community.WebExport.WebExport;
using Group = ILNumerics.Drawing.Group;

public partial class ExportTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(14)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(18)]
    [InlineData(19)]
    [InlineData(20)]
    [InlineData(21)]
    [InlineData(22)]
    public void SharedCatalogExportsWithoutRendering(int index)
    {
        using var scene = ExportDemoScenes.Create(index);
        var result = Exporter.ExportWithReport(scene);
        Assert.DoesNotContain("ilnumerics-export-placeholder", result.Html);
        var directory = Environment.GetEnvironmentVariable("WEBEXPORT_PREPARATION_FIXTURES");
        if (directory != null && index >= 14)
        {
            Directory.CreateDirectory(directory);
            File.WriteAllText(Path.Combine(directory, $"plot-{index}.html"), Exporter.ExportString(scene, HtmlRenderMode.Offline));
        }
    }

    [Fact]
    public void EmptyAndUnsupportedScenesProducePlaceholder()
    {
        using var scene = new Scene();
        Assert.Null(Exporter.GetChart(scene));
        Assert.Contains("ilnumerics-export-placeholder", Exporter.ExportString(scene));
        scene.Add(new PlotCube { new Label("Unsupported content") });
        Assert.Contains("ilnumerics-export-placeholder", Exporter.ExportString(scene));
        var path = Path.GetTempFileName();
        try
        {
            Exporter.ExportFile(scene, path);
            Assert.Equal(Exporter.ExportString(scene), File.ReadAllText(path));
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void InvalidArgumentsPropagate()
    {
        Assert.Throws<ArgumentNullException>(() => Exporter.ExportString(null!));
        Assert.Throws<ArgumentNullException>(() => Exporter.GetChart(null!));
        using var scene = new Scene();
        Assert.Throws<ArgumentException>(() => Exporter.ExportFile(scene, ""));
        Assert.Throws<DirectoryNotFoundException>(() => Exporter.ExportFile(scene, Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "chart.html")));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void LinesRetainCoordinatesAndDimension(bool twoD)
    {
        using var scope = Scope.Enter();
        Array<float> points = new float[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        using var scene = new Scene { new PlotCube(twoDMode: twoD) { new LinePlot(points) } };
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        if (twoD)
        {
            var trace = Assert.IsType<ScatterTrace>(Assert.Single(chart.Traces));
            Assert.Equal(new float[] { 1, 2, 3 }, trace.X);
            Assert.Equal(new float[] { 4, 5, 6 }, trace.Y);
        }
        else
        {
            var trace = Assert.IsType<Scatter3DTrace>(Assert.Single(chart.Traces));
            Assert.Equal(new float[] { 1, 2, 3 }, trace.X);
            Assert.Equal(new float[] { 4, 5, 6 }, trace.Y);
            Assert.Equal(new float[] { 7, 8, 9 }, trace.Z);
        }
        Assert.Contains("Plotly.newPlot", Exporter.ExportString(scene));
        Assert.DoesNotContain("ilnumerics-export-placeholder", Exporter.ExportString(scene));
    }

    [Fact]
    public void HiddenPlotsProducePlaceholder()
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube(twoDMode: true) { new LinePlot(new float[] { 1, 2, 3 }) { Visible = false } } };
        Assert.Null(Exporter.GetChart(scene));
        Assert.Contains("ilnumerics-export-placeholder", Exporter.ExportString(scene));
    }

    [Fact]
    public void AllPlotCubesAreExported()
    {
        using var scope = Scope.Enter();
        using var scene = new Scene
        {
            new PlotCube(twoDMode: true) { new LinePlot(new float[] { 1, 2, 3 }) }, new PlotCube(twoDMode: true) { new LinePlot(new float[] { 4, 5, 6 }) }, new PlotCube()
        };
        var html = Exporter.ExportString(scene);
        Assert.Equal(2, Regex.Matches(html, "Plotly.newPlot").Count);
        Assert.Single(Regex.Matches(html, "ilnumerics-export-placeholder"));
    }

    [Fact]
    public void NonSquareSurfaceRetainsCoordinates()
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube(twoDMode: false) { new Surface((x, y) => x + (10 * y), 1, 3, 3, 4, 5, 2) } };
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        var trace = Assert.IsType<SurfaceTrace>(Assert.Single(chart.Traces));
        Assert.Equal(new float[] { 1, 2, 3 }, trace.X);
        Assert.Equal(new float[] { 4, 5 }, trace.Y);
        Assert.Equal(new float?[] { 41, 42, 43 }, trace.Z![0]);
        Assert.Equal(new float?[] { 51, 52, 53 }, trace.Z[1]);
        Assert.Contains("Plotly.newPlot", Exporter.ExportString(scene));
    }

    [Fact]
    public void CurvilinearSurfaceProducesPlaceholderRatherThanDistortedData()
    {
        using var scope = Scope.Enter();
        Array<float> x = new float[,] { { 1, 2 }, { 3, 4 } };
        Array<float> y = new float[,] { { 5, 5 }, { 6, 6 } };
        Array<float> z = new float[,] { { 7, 8 }, { 9, 10 } };
        using var scene = new Scene { new PlotCube(twoDMode: false) { new Surface(z, x, y) } };
        Assert.Null(Exporter.GetChart(scene));
        Assert.Contains("ilnumerics-export-placeholder", Exporter.ExportString(scene));
    }

    [Fact]
    public void CurvilinearFastSurfaceProducesPlaceholder()
    {
        using var scope = Scope.Enter();
        Array<float> x = new float[,] { { 1, 2 }, { 3, 4 } };
        Array<float> y = new float[,] { { 5, 5 }, { 6, 6 } };
        Array<float> z = new float[,] { { 7, 8 }, { 9, 10 } };
        var surface = new FastSurface();
        surface.Update(x, y, z);
        using var scene = new Scene { new PlotCube(twoDMode: false) { surface } };
        Assert.Null(Exporter.GetChart(scene));
        Assert.Contains("ilnumerics-export-placeholder", Exporter.ExportString(scene));
    }

    [Fact]
    public void FastSurfaceRetainsCoordinates()
    {
        using var scope = Scope.Enter();
        var surface = new FastSurface();
        Array<float> x = new float[] { 1, 2, 3 };
        Array<float> y = new float[] { 4, 5 };
        Array<float> z = new float[,] { { 41, 42, 43 }, { 51, 52, 53 } };
        surface.Update(x, y, z, colormap: Colormaps.Jet);
        using var scene = new Scene { new PlotCube(twoDMode: false) { surface } };
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        var trace = Assert.IsType<SurfaceTrace>(Assert.Single(chart.Traces));
        Assert.Equal(new float[] { 1, 2, 3 }, trace.X);
        Assert.Equal(new float[] { 4, 5 }, trace.Y);
        Assert.Equal(new float?[] { 41, 42, 43 }, trace.Z![0]);
        Assert.Equal(new float?[] { 51, 52, 53 }, trace.Z[1]);
        Assert.Equal("Jet", trace.Colorscale);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void LogarithmicLineRetainsDataCoordinates(bool configure)
    {
        using var scope = Scope.Enter();
        Array<float> points = new float[,] { { 1, 10, 100 }, { 2, 20, 200 }, { 0, 0, 0 } };
        var cube = new PlotCube(twoDMode: true) { new LinePlot(points) };
        cube.ScaleModes.XAxisScale = AxisScale.Logarithmic;
        cube.ScaleModes.YAxisScale = AxisScale.Logarithmic;
        using var scene = new Scene { cube };
        if (configure)
            scene.Configure();
        var trace = Assert.IsType<ScatterTrace>(Assert.Single(Exporter.GetChart(scene)!.Traces));
        Assert.Equal(new float[] { 1, 10, 100 }, trace.X);
        for (var i = 0; i < 3; i++)
            Assert.Equal(new float[] { 2, 20, 200 }[i], trace.Y![i], 3);
    }

    [Fact]
    public void HtmlPreservesUnicodeAndNonFiniteGapsAndEscapesIds()
    {
        var chart = new Chart(new ScatterTrace { Name = "Größe 日本語", X = new float[] { 1, 2, 3 }, Y = new[] { Single.NaN, Single.PositiveInfinity, 3 } });
        var html = chart.RenderPartial("x'</script><script>alert(1)</script>");
        Assert.DoesNotContain("</script><script>alert", html);
        Assert.Contains("TextDecoder('utf-8'", html);
        var match = Regex.Match(html, "atob\\('([^']+)'\\)");
        Assert.True(match.Success);
        using var json = JsonDocument.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(match.Groups[1].Value)));
        var trace = json.RootElement.GetProperty("traces")[0];
        Assert.Equal("Größe 日本語", trace.GetProperty("name").GetString());
        Assert.Equal(JsonValueKind.Null, trace.GetProperty("y")[0].ValueKind);
        Assert.Equal(JsonValueKind.Null, trace.GetProperty("y")[1].ValueKind);
        Assert.Equal(3, trace.GetProperty("y")[2].GetSingle());
        Assert.Contains("return Plotly.newPlot", html);
        Assert.Contains("HTML chart could not be loaded.", html);
    }

    [Fact]
    public void ScalarImagePreservesRowsColumnsColorRangeAndMissingValues()
    {
        using var scope = Scope.Enter();
        Array<float> data = new[,] { { 1, 2, 3 }, { 4, Single.NaN, 6 } };
        using var scene = new Scene { new PlotCube { new ImageSCPlot(data, Colormaps.Jet, Tuple.Create(0f, 10f)) } };
        var trace = Assert.IsType<HeatmapTrace>(Assert.Single(Exporter.GetChart(scene)!.Traces));
        Assert.Equal(new float[] { 0, 1, 2 }, trace.X);
        Assert.Equal(new float[] { 0, 1 }, trace.Y);
        Assert.Equal(new float?[] { 1, 2, 3 }, trace.Z[0]);
        Assert.Equal(new float?[] { 4, null, 6 }, trace.Z[1]);
        Assert.Equal(0, trace.ZMin);
        Assert.Equal(10, trace.ZMax);
        Assert.NotEmpty(trace.Colorscale);
        Assert.Empty(Exporter.ExportWithReport(scene).Issues);
    }

    [Fact]
    public void ContourExportsActualLineGeometryAndReportsMissingDecorations()
    {
        using var scope = Scope.Enter();
        Array<float> data = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
        var contour = new ContourPlot(data);
        using var scene = new Scene { new PlotCube { contour } };
        var source = contour.Find<LineStrip>().First();
        Array<float> positions = source.Positions.Storage;
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        var trace = Assert.IsType<ScatterTrace>(chart.Traces[0]);
        Assert.Equal(positions.GetValue(0, 0), trace.X[0]);
        Assert.Equal(positions.GetValue(1, 0), trace.Y[0]);
        var result = Exporter.ExportWithReport(scene);
        Assert.Contains(result.Issues, issue => issue.NodeType == nameof(ContourPlot) && issue.Message.Contains("labels"));
        Assert.Contains("ilnumerics-export-warning", result.Html);
    }

    [Fact]
    public void IndependentLinesKeepSegmentBreaksAndPointsUseMarkers()
    {
        using var scope = Scope.Enter();
        var lines = new Lines();
        lines.Positions.Update(new float[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 0, 0, 0, 0 } });
        var points = new Points();
        points.Positions.Update(new float[,] { { 9, 10 }, { 11, 12 }, { 0, 0 } });
        using var scene = new Scene { new PlotCube { lines, points } };
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        Assert.Equal(2, chart.Traces.Count);
        var trace = Assert.IsType<ScatterTrace>(chart.Traces[0]);
        Assert.Equal(new[] { 1, 2, Single.NaN, 3, 4, Single.NaN }, trace.X);
        Assert.Equal(ScatterMode.Markers, Assert.IsType<ScatterTrace>(chart.Traces[1]).Mode);
    }

    [Fact]
    public void PartialExportReportsUnsupportedContentWithCubeIndex()
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube { new LinePlot(new float[] { 1, 2 }) }, new PlotCube { new LinePlot(new float[] { 3, 4 }), new Label("<missing>") } };
        var result = Exporter.ExportWithReport(scene);
        var issue = Assert.Single(result.Issues);
        Assert.Equal(1, issue.PlotCubeIndex);
        Assert.Equal(nameof(Label), issue.NodeType);
        Assert.Contains("Plot 2", result.Html);
        Assert.DoesNotContain("ilnumerics-export-placeholder", result.Html);
    }

    [Fact]
    public void HiddenGroupsAreNeitherExportedNorReportedAsMissing()
    {
        using var scope = Scope.Enter();
        var hidden = new Group { new LinePlot(new float[] { 3, 4 }), new Label("Hidden") };
        hidden.Visible = false;
        using var scene = new Scene { new PlotCube { new LinePlot(new float[] { 1, 2 }), hidden } };
        Assert.Single(Exporter.GetChart(scene)!.Traces);
        Assert.Empty(Exporter.ExportWithReport(scene).Issues);
    }

    [Fact]
    public void OfflineExportEmbedsPinnedLibraryOnceAndHasNoCdnLoader()
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube { new LinePlot(new float[] { 1, 2 }) }, new PlotCube { new LinePlot(new float[] { 3, 4 }) } };
        var html = Exporter.ExportString(scene, HtmlRenderMode.Offline);
        Assert.Single(Regex.Matches(html, "plotly.js v3.3.1"));
        Assert.DoesNotContain("loadScript('https:", html);
        Assert.DoesNotContain("MathJax.js?config", html);
        var path = Path.GetTempFileName();
        try
        {
            Exporter.ExportFile(scene, path, HtmlRenderMode.Offline);
            Assert.Contains("plotly.js v3.3.1", File.ReadAllText(path));
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void RenderModesAreValidatedAndExistingModeDoesNotInjectLibraries()
    {
        var chart = new Chart(new ScatterTrace());
        Assert.Throws<ArgumentOutOfRangeException>(() => chart.Render(null, (HtmlRenderMode) 99));
        var html = chart.RenderPartial("chart", HtmlRenderMode.Existing);
        Assert.DoesNotContain("loadScript('https:", html);
        Assert.DoesNotContain("plotly.js v3.3.1", html);
        using var scene = new Scene();
        Assert.Throws<ArgumentOutOfRangeException>(() => Exporter.ExportWithReport(scene, (HtmlRenderMode) 99));
    }

    [Fact]
    public void SceneContentOutsidePlotCubesIsReported()
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube { new LinePlot(new float[] { 1, 2 }) }, new Label("Outside") };
        var issue = Assert.Single(Exporter.ExportWithReport(scene).Issues);
        Assert.Equal(-1, issue.PlotCubeIndex);
        Assert.Equal(nameof(Label), issue.NodeType);
    }

    [Fact]
    public void OfflineBrowserFixtureContainsAllSupportedTraceFamilies()
    {
        using var scope = Scope.Enter();
        Array<float> data = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
        using var scene = new Scene
        {
            new PlotCube { new ImageSCPlot(data) }, new PlotCube { new ContourPlot(data) }, new PlotCube { new LinePlot(new float[] { 1, 2, 3 }) },
            new PlotCube(twoDMode: false) { new Surface((x, y) => x + y, 0, 2, 3, 0, 2, 3) }
        };
        var html = Exporter.ExportString(scene, HtmlRenderMode.Offline);
        Assert.Equal(4, Regex.Matches(html, "return Plotly.newPlot").Count);

        // Optional artifact consumed by BrowserSmoke.cjs; normal unit runs need no browser.
        var path = Environment.GetEnvironmentVariable("WEBEXPORT_OFFLINE_SMOKE_PATH");
        if (!String.IsNullOrEmpty(path))
            File.WriteAllText(path, html);
    }

    [Fact]
    public void TransformedGroupsProduceDiagnosticsInsteadOfUntransformedData()
    {
        using var scope = Scope.Enter();
        var group = new Group(translate: new Vector3(1, 2, 3)) { new LinePlot(new float[] { 1, 2 }) };
        using var scene = new Scene { new PlotCube { group } };
        var result = Exporter.ExportWithReport(scene);
        Assert.Contains(result.Issues, issue => issue.Message.Contains("Transformed"));
        Assert.Contains("ilnumerics-export-placeholder", result.Html);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ErrorBarsPreserveAsymmetricExtentsAndCaps(bool vertical)
    {
        using var scope = Scope.Enter();
        Array<float> x = new float[,] { { 1, 2 } };
        Array<float> y = new float[,] { { 4, 6 } };
        Array<float> lower = new float[,] { { 1, 2 } };
        Array<float> upper = new float[,] { { 2, 3 } };
        var plot = new ErrorBarPlot(x, y, lower, upper, vertical);
        using var scene = new Scene { new PlotCube { plot } };
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        Assert.Equal(2, chart.Traces.Count);
        var errors = Assert.IsType<ScatterTrace>(chart.Traces[1]);
        Array<float> source = plot.ErrorBar.Positions.Storage;
        for (var vertex = 0; vertex < source.S[1]; vertex++)
        {
            var exported = vertex + (vertex / 2);
            Assert.Equal(source.GetValue(0, vertex), errors.X[exported]);
            Assert.Equal(source.GetValue(1, vertex), errors.Y[exported]);
        }
        Assert.True(Single.IsNaN(errors.X[2]));
        Assert.Empty(Exporter.ExportWithReport(scene).Issues);
    }

    [Theory]
    [InlineData("box")]
    [InlineData("candlestick")]
    [InlineData("area")]
    public void StatisticalPlotsExportTheirFilledGeometry(string kind)
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube { CreateStandardPlot(kind) } };
        var chart = Assert.IsType<Chart>(Exporter.GetChart(scene));
        Assert.Contains(chart.Traces, trace => trace is ScatterTrace { Fill: "toself" });
        Assert.Contains(chart.Traces, trace => trace is ScatterTrace { Fill: null });
        Assert.DoesNotContain(Exporter.ExportWithReport(scene).Issues, issue => issue.Message.Contains("Unsupported"));
        Assert.DoesNotContain("ilnumerics-export-placeholder", Exporter.ExportString(scene));
    }

    [Fact]
    public void BoxGeometryPreservesComputedQuartilesAndWhiskers()
    {
        using var scope = Scope.Enter();
        using var scene = new Scene { new PlotCube { CreateStandardPlot("box") } };
        var traces = Exporter.GetChart(scene)!.Traces.OfType<ScatterTrace>().ToArray();
        var fills = traces.Where(trace => trace.Fill == "toself").ToArray();
        Assert.NotEmpty(fills);
        Assert.Equal(2f, fills.SelectMany(trace => trace.Y).Where(Single.IsFinite).Min());
        Assert.Equal(8f, fills.SelectMany(trace => trace.Y).Where(Single.IsFinite).Max());
        Assert.Equal(1f, traces.Where(trace => trace.Fill == null).SelectMany(trace => trace.Y).Where(Single.IsFinite).Min());
        Assert.Equal(9f, traces.Where(trace => trace.Fill == null).SelectMany(trace => trace.Y).Where(Single.IsFinite).Max());
    }

    [Fact]
    public void IndexedTriangleStripExportsExactMeshTopology()
    {
        using var scope = Scope.Enter();
        var shape = new TrianglesStrip();
        shape.Positions.Update(new float[,] { { 0, 1, 0, 1 }, { 0, 0, 1, 1 }, { 2, 2, 2, 2 } });
        shape.Indices.Update(new[] { 0, 1, 2, 3 });
        using var scene = new Scene { new PlotCube(twoDMode: false) { shape } };
        var trace = Assert.IsType<Mesh3DTrace>(Assert.Single(Exporter.GetChart(scene)!.Traces));
        Assert.Equal(new[] { 0, 3 }, trace.I);
        Assert.Equal(new[] { 1, 4 }, trace.J);
        Assert.Equal(new[] { 2, 5 }, trace.K);
        Assert.Equal(new float[] { 0, 1, 0, 0, 1, 1 }, trace.X);
        Assert.Equal(new float[] { 0, 0, 1, 1, 0, 1 }, trace.Y);
    }

    [Fact]
    public void TriangleStripNaNSeparatorsDoNotJoinDisconnectedPolygons()
    {
        using var scope = Scope.Enter();
        var shape = new TrianglesStrip();
        shape.Positions.Update(new[,] { { 0, 1, 0, Single.NaN, 10, 11, 10 }, { 0, 0, 1, Single.NaN, 0, 0, 1 }, { 0, 0, 0, Single.NaN, 0, 0, 0 } });
        using var scene = new Scene { new PlotCube { shape } };
        var triangles = Exporter.GetChart(scene)!.Traces.Cast<ScatterTrace>().ToArray();
        Assert.Equal(2, triangles.Length);
        Assert.Equal(new[] { 0, 1, 0, 0, Single.NaN }, triangles[0].X);
        Assert.Equal(new[] { 10, 11, 10, 10, Single.NaN }, triangles[1].X);
    }

    [Fact]
    public void UnpreparedBarsAndSplinesGenerateGeometry()
    {
        using var scope = Scope.Enter();
        using var bars = new Scene { new PlotCube { new BarPlot((Array<float>) new float[,] { { 1, 2 } }) } };
        Assert.DoesNotContain("ilnumerics-export-placeholder", Exporter.ExportString(bars));
        using var spline = new Scene { new PlotCube { new SplinePlot(new float[,] { { 1, 2, 3, 4 }, { 4, 2, 6, 3 }, { 0, 0, 0, 0 } }) } };
        Assert.DoesNotContain(Exporter.ExportWithReport(spline).Issues, issue => issue.Message.Contains("control polygon"));
    }

    private static Group CreateStandardPlot(string kind)
    {
        using var scope = Scope.Enter();
        Array<float> x = new float[,] { { 1 } };
        Array<float> samples = new float[,] { { 1 }, { 3 }, { 5 }, { 7 }, { 9 } };
        return kind switch
        {
            "box" => new BoxPlot(x, samples),
            "candlestick" => new Candlestick((Array<float>) new float[,] { { 1, 2 }, { 2, 3 }, { 8, 9 }, { 4, 7 }, { 6, 5 } }),
            "area" => new StackedAreaPlot((Array<float>) new float[,] { { 1, 2, 3 } }, new float[,] { { 1, 2, 3 }, { 2, 3, 4 } }),
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };
    }

    [Fact]
    public void StandardPlotBrowserFixtureExportsWithoutAWindow()
    {
        using var scope = Scope.Enter();
        var mesh = new Triangles();
        mesh.Positions.Update(new float[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 0, 1, 2 } });
        using var scene = new Scene
        {
            new PlotCube { new ErrorBarPlot((Array<float>) new float[,] { { 1, 2 } }, new float[,] { { 4, 6 } }, new float[,] { { 1, 2 } }, new float[,] { { 2, 3 } }) },
            new PlotCube { CreateStandardPlot("box") }, new PlotCube { CreateStandardPlot("candlestick") }, new PlotCube { CreateStandardPlot("area") },
            new PlotCube(twoDMode: false) { mesh }
        };
        var html = Exporter.ExportString(scene, HtmlRenderMode.Offline);
        Assert.DoesNotContain("ilnumerics-export-placeholder", html);
        var path = Environment.GetEnvironmentVariable("WEBEXPORT_STANDARD_SMOKE_PATH");
        if (!String.IsNullOrEmpty(path))
            File.WriteAllText(path, html);
    }
}
