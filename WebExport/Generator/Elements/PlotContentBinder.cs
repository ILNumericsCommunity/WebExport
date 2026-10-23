using System;
using System.Collections.Generic;
using System.Drawing;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Traces;
using CSPlotlySlim.Chart.Traces.Properties;
using ILNumerics.Community.ExportPreparation;
using ILNumerics.Drawing;
using Line = CSPlotlySlim.Chart.Generic.Line;
using Marker = CSPlotlySlim.Chart.Generic.Marker;

namespace ILNumerics.Community.WebExport.Generator.Elements;

/// <summary>Converts supported scene geometry into an export snapshot without a UI.</summary>
internal sealed partial class PlotContentBinder(PlotCube cube, Layout layout, List<Trace> traces, List<ExportIssue> issues)
{
    /// <summary>Records an omitted or approximated node for the current plot cube.</summary>
    private void Issue(Node node, string message) => issues.Add(new ExportIssue(0, node.GetType().Name, message));

    /// <summary>Dispatches visible supported nodes to geometry converters and reports omissions.</summary>
    public void Bind(Node node)
    {
        if (!node.Visible)
            return;
        if (node is Group transformed && !transformed.Transform.Equals(Matrix4.Identity))
        {
            Issue(node, "Transformed plot groups are not supported and were omitted.");
            return;
        }

        var before = traces.Count;
        switch (node)
        {
            case SplinePlot spline:
                if (spline.SmoothLine.Visible)
                    BindShape(spline.SmoothLine);
                var lineVisible = spline.Line.Visible;
                spline.Line.Visible = false;
                try
                {
                    new LinePlotBinder().Bind(spline, cube, layout, traces);
                }
                finally
                {
                    spline.Line.Visible = lineVisible;
                }
                break;
            case ErrorBarPlot errors:
                Bind(errors.LinePlot);
                if (errors.ErrorBar.Visible)
                    BindShape(errors.ErrorBar);
                break;
            case BarPlot bar:
                if (bar.Fill.Visible)
                    BindTriangles(bar.Fill);
                foreach (var border in HeadlessPlotPreparation.Borders(bar.Border))
                    BindShape(border);
                break;
            case BarPlotEx bar:
                if (bar.Fill.Visible)
                    BindTriangles(bar.Fill);
                foreach (var border in HeadlessPlotPreparation.Borders(bar.Border))
                    BindShape(border);
                break;
            case BoxPlot or Candlestick or StackedAreaPlot or FillArea:
                BindStandardGeometry((Group) node);
                Issue(node, "Rendered geometry is exported; statistical hover fields, custom labels and legend semantics are not retained.");
                break;
            case LinePlot line:
                new LinePlotBinder().Bind(line, cube, layout, traces);
                break;
            case Surface or FastSurface:
                new SurfaceBinder().Bind((Group) node, cube, layout, traces);
                break;
            case ImageSCPlot image:
                BindImage(image);
                break;
            case ContourPlot contour:
                foreach (var line in contour.Find<LineStrip>())
                {
                    if (VisibleToRoot(line, contour))
                        BindShape(line);
                }
                Issue(node, "Contour line geometry is exported; filled regions and labels are omitted.");
                break;
            case Triangles or TrianglesStrip or TrianglesFan:
                BindTriangles((Shape) node);
                break;
            case Lines or Points:
                BindShape((Shape) node);
                break;
            case Group group when group.GetType() == typeof(Group):
                foreach (var child in group.Children)
                    Bind(child);
                return;
            default:
                Issue(node, "Unsupported content was omitted.");
                return;
        }

        if (before == traces.Count)
            Issue(node, "No supported visible data could be converted.");
    }

    /// <summary>Checks effective visibility from a node through its containing plot.</summary>
    private static bool VisibleToRoot(Node node, Node root)
    {
        for (var current = node; current != null; current = current.Parent)
        {
            if (!current.Visible)
                return false;
            if (ReferenceEquals(current, root))
                break;
        }

        return true;
    }

    /// <summary>Exports finite scalar image samples and their sampled color scale on linear two-dimensional axes.</summary>
    private void BindImage(ImageSCPlot image)
    {
        using var scope = Scope.Enter();
        if (!cube.TwoDMode || cube.ScaleModes.XAxisScale != AxisScale.Linear || cube.ScaleModes.YAxisScale != AxisScale.Linear)
        {
            Issue(image, "Scalar images currently require linear 2D axes.");
            return;
        }

        Array<float> values = image.Values;
        if (isempty(values))
            return;
        var trace = new HeatmapTrace { Name = "Image", ZMin = image.MinMaxDataRange.Item1, ZMax = image.MinMaxDataRange.Item2 };
        for (var x = 0; x < image.NumberColumns; x++)
            trace.X.Add(x);
        for (var y = 0; y < image.NumberRows; y++)
        {
            trace.Y.Add(y);
            var row = new List<float?>();
            for (var x = 0; x < image.NumberColumns; x++)
            {
                var value = values.GetValue(y, x);
                row.Add(Single.IsFinite(value) ? value : null);
            }

            trace.Z.Add(row);
        }

        for (var i = 0; i <= 64; i++)
        {
            var t = i / 64f;
            var color = image.Colormap.Map(t, Tuple.Create(0f, 1f));
            var rgba = FormattableString.Invariant($"rgba({Math.Round(color.X * 255)},{Math.Round(color.Y * 255)},{Math.Round(color.Z * 255)},{color.W})");
            trace.Colorscale.Add([t, rgba]);
        }

        traces.Add(trace);
    }

    /// <summary>Converts indexed point or line primitives, retaining discontinuities between independent segments.</summary>
    private void BindShape(Shape shape)
    {
        if (!shape.Visible)
            return;
        using var scope = Scope.Enter();
        Array<float> positions = shape.Positions.Storage;
        if (isempty(positions))
            return;
        var x = new List<float>();
        var y = new List<float>();
        var z = new List<float>();
        var independentLines = shape is Lines && shape is not LineStrip;
        var order = VertexOrder(shape, positions);
        for (var item = 0; item < order.Length; item++)
        {
            var i = order[item];
            x.Add(Unscale(positions.GetValue(0, i), cube.ScaleModes.XAxisScale));
            y.Add(Unscale(positions.GetValue(1, i), cube.ScaleModes.YAxisScale));
            z.Add(Unscale(positions.GetValue(2, i), cube.ScaleModes.ZAxisScale));
            if (independentLines && item % 2 == 1)
            {
                x.Add(Single.NaN);
                y.Add(Single.NaN);
                z.Add(Single.NaN);
            }
        }

        var mode = shape is Points ? ScatterMode.Markers : ScatterMode.Lines;
        var line = shape is Lines lines ? new Line { Color = lines.Color ?? Color.Black, Width = lines.Width, Dash = lines.DashStyle.ToPlotlyDashStyle() } : null;
        var marker = shape is Points points ? new Marker { Color = points.Color ?? Color.Black, Size = Math.Max(1, (int) Math.Round(points.Size)) } : null;
        if (cube.TwoDMode)
            traces.Add(new ScatterTrace { X = x, Y = y, Mode = mode, Line = line, Marker = marker, ShowLegend = false });
        else
            traces.Add(new Scatter3DTrace { X = x, Y = y, Z = z, Mode = mode, Line = line, Marker = marker, ShowLegend = false });
        if (!isempty(shape.Colors.Storage))
            Issue(shape, "Per-vertex colors are approximated by a single trace color.");
    }

    /// <summary>Converts internal logarithmic plot coordinates back to data coordinates.</summary>
    private static float Unscale(float value, AxisScale scale) => scale == AxisScale.Logarithmic ? (float) Math.Pow(10, value) : value;
}
