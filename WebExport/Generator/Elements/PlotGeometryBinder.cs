using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CSPlotlySlim.Chart.Traces;
using CSPlotlySlim.Chart.Traces.Properties;
using ILNumerics.Drawing;
using Line = CSPlotlySlim.Chart.Generic.Line;

namespace ILNumerics.Community.WebExport.Generator.Elements;

/// <summary>Converts supported scene geometry into an export snapshot without a UI.</summary>
internal sealed partial class PlotContentBinder
{
    /// <summary>Visits the prepared primitive children of a standard compound plot.</summary>
    private void BindStandardGeometry(Group group)
    {
        using var scope = Scope.Enter();
        foreach (var child in group.Children)
        {
            if (!child.Visible || (child is Shape empty && isempty(empty.Positions.Storage)))
                continue;
            if (child is FillArea area)
                BindStandardGeometry(area);
            else if (child is Group nested && nested.GetType() == typeof(Group))
                Bind(child);
            else if (child is Shape or LinePlot or FillArea)
                Bind(child);
            else
                Issue(child, "Unsupported decoration in standard plot was omitted.");
        }
    }

    /// <summary>Returns validated vertex indices, synthesizing sequential indices for non-indexed shapes.</summary>
    private static int[] VertexOrder(Shape shape, Array<float> positions)
    {
        Array<int> indices = shape.Indices.Storage;
        if (isempty(indices))
            return Enumerable.Range(0, (int) positions.S[1]).ToArray();
        var order = indices.ToArray();
        if (order.Any(i => i < 0 || i >= positions.S[1]))
            throw new InvalidOperationException("Plot geometry contains an out-of-range vertex index.");
        return order;
    }

    /// <summary>Converts indexed triangle lists, strips, or fans while preserving separator runs and face colors.</summary>
    private void BindTriangles(Shape shape)
    {
        using var scope = Scope.Enter();
        Array<float> positions = shape.Positions.Storage;
        if (isempty(positions))
            return;
        var order = VertexOrder(shape, positions);
        Array<float> colors = shape.Colors.Storage;
        var polygons = new List<ScatterTrace>();
        var mesh = new Mesh3DTrace { ShowLegend = false };
        var run = new List<int>();
        var approximatedColors = false;
        foreach (var vertex in order)
        {
            if (!Single.IsFinite(positions.GetValue(0, vertex)) || !Single.IsFinite(positions.GetValue(1, vertex)) || !Single.IsFinite(positions.GetValue(2, vertex)))
            {
                EmitRun();
                run.Clear();
            }
            else
                run.Add(vertex);
        }

        EmitRun();
        if (cube.TwoDMode)
            traces.AddRange(polygons);
        else if (mesh.I.Count > 0)
            traces.Add(mesh);
        if (approximatedColors)
            Issue(shape, "Interpolated vertex colors are approximated by one color per triangle.");

        void EmitRun()
        {
            if (shape is TrianglesStrip)
            {
                for (var i = 2; i < run.Count; i++)
                    Emit(run[i % 2 == 0 ? i - 2 : i - 1], run[i % 2 == 0 ? i - 1 : i - 2], run[i]);
            }
            else if (shape is TrianglesFan)
            {
                for (var i = 2; i < run.Count; i++)
                    Emit(run[0], run[i - 1], run[i]);
            }
            else
            {
                for (var i = 2; i < run.Count; i += 3)
                    Emit(run[i - 2], run[i - 1], run[i]);
            }
        }

        void Emit(int a, int b, int c)
        {
            if (a == b || a == c || b == c)
                return;
            var color = VertexColor(a);
            approximatedColors |= color != VertexColor(b) || color != VertexColor(c);
            if (cube.TwoDMode)
            {
                // Separate triangles preserve GPU-style overlap. A single SVG fill path can
                // cancel overlapping subpaths, leaving holes in box/candlestick fills.
                var trace = new ScatterTrace { Fill = "toself", FillColor = color, Mode = ScatterMode.Lines, Line = new Line { Width = 0, Color = color }, ShowLegend = false };
                polygons.Add(trace);
                foreach (var vertex in new[] { a, b, c, a })
                {
                    trace.X.Add(Unscale(positions.GetValue(0, vertex), cube.ScaleModes.XAxisScale));
                    trace.Y.Add(Unscale(positions.GetValue(1, vertex), cube.ScaleModes.YAxisScale));
                }
                trace.X.Add(Single.NaN);
                trace.Y.Add(Single.NaN);
            }
            else
            {
                var offset = mesh.X.Count;
                foreach (var vertex in new[] { a, b, c })
                {
                    mesh.X.Add(Unscale(positions.GetValue(0, vertex), cube.ScaleModes.XAxisScale));
                    mesh.Y.Add(Unscale(positions.GetValue(1, vertex), cube.ScaleModes.YAxisScale));
                    mesh.Z.Add(Unscale(positions.GetValue(2, vertex), cube.ScaleModes.ZAxisScale));
                }
                mesh.I.Add(offset);
                mesh.J.Add(offset + 1);
                mesh.K.Add(offset + 2);
                mesh.FaceColor.Add(FormattableString.Invariant($"rgba({color.R},{color.G},{color.B},{color.A / 255f})"));
            }
        }

        Color VertexColor(int vertex)
        {
            if (shape.Color.HasValue || isempty(colors))
                return shape.Color ?? Color.Gray;
            var column = colors.S[1] == 1 ? 0 : vertex;
            if (column >= colors.S[1])
                throw new InvalidOperationException("Plot geometry has too few vertex colors.");

            int Channel(int row) => (int) Math.Round(Math.Clamp(colors.GetValue(row, column), 0, 1) * 255);

            return Color.FromArgb(Channel(3), Channel(0), Channel(1), Channel(2));
        }
    }
}
