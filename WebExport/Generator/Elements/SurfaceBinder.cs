using System;
using System.Collections.Generic;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Traces;
using ILNumerics.Drawing;
using FastSurface = ILNumerics.Drawing.Plotting.FastSurface;
using Surface = ILNumerics.Drawing.Plotting.Surface;

namespace ILNumerics.Community.WebExport.Generator.Elements;

/// <summary>
/// Binds an ILNumerics <see cref="Surface" /> to a Plotly <see cref="SurfaceTrace" />.
/// </summary>
public class SurfaceBinder : IPlotBinder
{
    #region Implementation of IPlotBinder

    /// <inheritdoc />
    public void Bind(Group group, Group root, Layout layout, List<Trace> traces)
    {
        using var scope = Scope.Enter();

        if (root is not PlotCube plotCube)
            return;
        var scaleModes = plotCube.ScaleModes;
        var traceIndex = traces.Count;

        if (group is Surface surface)
        {
            if (!TryCreateFromSurface(surface, scaleModes, traceIndex, out var trace))
                return;

            traces.Add(trace);
            return;
        }

        if (group is FastSurface fastSurface)
        {
            if (!TryCreateFromFastSurface(fastSurface, scaleModes, traceIndex, out var trace))
                return;

            traces.Add(trace);
        }
    }

    #endregion

    #region Private

    /// <summary>Maps supported native color-map names to their Plotly equivalents.</summary>
    private static string? ResolveColorscale(Colormap? colormap)
    {
        if (colormap == null)
            return null;

        var name = colormap.Type.ToString();
        if (name.EndsWith("Colormap", StringComparison.OrdinalIgnoreCase))
            name = name[..^"Colormap".Length];

        return name.ToLowerInvariant() switch
        {
            "jet" => "Jet",
            "summer" => "Summer",
            "hot" => "Hot",
            "cool" => "Cool",
            "greys" => "Greys",
            "gray" => "Greys",
            "blues" => "Blues",
            "rainbow" => "Rainbow",
            "plasma" => "Plasma",
            "viridis" => "Viridis",
            "cividis" => "Cividis",
            "ylgnbu" => "YlGnBu",
            "ylorrd" => "YlOrRd",
            _ => null
        };
    }

    /// <summary>Converts a rectilinear native surface, retaining missing heights as gaps.</summary>
    private static bool TryCreateFromSurface(Surface surface, ScaleModes scaleModes, int traceIndex, out SurfaceTrace trace)
    {
        trace = new SurfaceTrace();

        Array<float> positions = surface.Positions; // n x n x 3 (z, x, y)
        if (isempty(positions))
            return false;

        int rows;
        int cols;
        int depth;
        rows = (int) positions.S[0];
        cols = (int) positions.S[1];
        depth = (int) positions.S[2];

        if (rows <= 0 || cols <= 0 || depth < 3)
            return false;

        var xValues = new List<float>(cols);
        for (var j = 0; j < cols; j++)
        {
            var x = GetScaledValue(positions.GetValue(0, j, 1), scaleModes.XAxisScale);
            if (!x.HasValue)
                return false;
            xValues.Add(x.Value);
        }

        trace.X = xValues;

        var yValues = new List<float>(rows);
        for (var i = 0; i < rows; i++)
        {
            var y = GetScaledValue(positions.GetValue(i, 0, 2), scaleModes.YAxisScale);
            if (!y.HasValue)
                return false;
            yValues.Add(y.Value);
        }

        trace.Y = yValues;

        var zMatrix = new List<IList<float?>>(rows);
        for (var i = 0; i < rows; i++)
        {
            var zRow = new List<float?>(cols);
            for (var j = 0; j < cols; j++)
            {
                // Plotly's vector X/Y representation requires a rectilinear grid.
                if (positions.GetValue(i, j, 1) != positions.GetValue(0, j, 1)
                    || positions.GetValue(i, j, 2) != positions.GetValue(i, 0, 2))
                    return false;
                var z = GetScaledValue(positions.GetValue(i, j, 0), scaleModes.ZAxisScale);
                zRow.Add(z);
            }
            zMatrix.Add(zRow);
        }

        trace.Z = zMatrix;

        var colorscale = ResolveColorscale(surface.Colormap);
        trace.Colorscale = colorscale ?? "Viridis";
        trace.ShowScale = true;
        trace.Name = $"Surface {traceIndex}";
        trace.ShowLegend = false;

        return true;
    }

    /// <summary>Reconstructs a rectangular grid from native fast-surface vertices without inventing missing samples.</summary>
    private static bool TryCreateFromFastSurface(FastSurface fastSurface, ScaleModes scaleModes, int traceIndex, out SurfaceTrace trace)
    {
        trace = new SurfaceTrace();

        Array<float> positions = fastSurface.Fill.Positions.Storage; // 3 (x, y, z) x n or 3 x rows x cols
        if (isempty(positions))
            return false;

        if (positions.S.NumberOfDimensions == 3)
        {
            var depth = (int) positions.S[0];
            var rows = (int) positions.S[1];
            var cols = (int) positions.S[2];

            if (depth < 3 || rows <= 0 || cols <= 0)
                return false;

            var xValues = new List<float>(cols);
            for (var j = 0; j < cols; j++)
            {
                var x = GetScaledValue(positions.GetValue(0, 0, j), scaleModes.XAxisScale);
                if (!x.HasValue)
                    return false;
                xValues.Add(x.Value);
            }

            var yValues = new List<float>(rows);
            for (var i = 0; i < rows; i++)
            {
                var y = GetScaledValue(positions.GetValue(1, i, 0), scaleModes.YAxisScale);
                if (!y.HasValue)
                    return false;
                yValues.Add(y.Value);
            }

            var zMatrix = new List<IList<float?>>(rows);
            for (var i = 0; i < rows; i++)
            {
                var zRow = new List<float?>(cols);
                for (var j = 0; j < cols; j++)
                {
                    if (positions.GetValue(0, i, j) != positions.GetValue(0, 0, j)
                        || positions.GetValue(1, i, j) != positions.GetValue(1, i, 0))
                        return false;
                    var z = GetScaledValue(positions.GetValue(2, i, j), scaleModes.ZAxisScale);
                    zRow.Add(z);
                }
                zMatrix.Add(zRow);
            }

            trace.X = xValues;
            trace.Y = yValues;
            trace.Z = zMatrix;
        }
        else
        {
            var depth = (int) positions.S[0];
            var count = (int) positions.S[1];
            if (depth < 3 || count <= 0)
                return false;

            var xValues = new List<float>();
            var yValues = new List<float>();
            var xIndex = new Dictionary<float, int>();
            var yIndex = new Dictionary<float, int>();
            var values = new Dictionary<(int X, int Y), float?>();
            for (var i = 0; i < count; i++)
            {
                var x = GetScaledValue(positions.GetValue(0, i), scaleModes.XAxisScale);
                var y = GetScaledValue(positions.GetValue(1, i), scaleModes.YAxisScale);
                var z = GetScaledValue(positions.GetValue(2, i), scaleModes.ZAxisScale);
                if (!x.HasValue || !y.HasValue)
                    continue; // FastSurface includes NaN separator vertices.
                if (!xIndex.TryGetValue(x.Value, out var xi))
                {
                    xi = xValues.Count;
                    xValues.Add(x.Value);
                    xIndex.Add(x.Value, xi);
                }
                if (!yIndex.TryGetValue(y.Value, out var yi))
                {
                    yi = yValues.Count;
                    yValues.Add(y.Value);
                    yIndex.Add(y.Value, yi);
                }
                if (values.TryGetValue((xi, yi), out var existing) && existing != z)
                    return false;
                values[(xi, yi)] = z;
            }

            // Reject non-rectilinear data before allocating an artificial Cartesian grid.
            if (values.Count == 0 || (long) xValues.Count * yValues.Count != values.Count)
                return false;
            var zMatrix = new List<IList<float?>>(yValues.Count);
            for (var yi = 0; yi < yValues.Count; yi++)
            {
                var row = new List<float?>(xValues.Count);
                for (var xi = 0; xi < xValues.Count; xi++)
                    row.Add(values[(xi, yi)]);
                zMatrix.Add(row);
            }

            trace.X = xValues;
            trace.Y = yValues;
            trace.Z = zMatrix;
        }

        var colorscale = ResolveColorscale(fastSurface.Colormap);
        trace.Colorscale = colorscale ?? "Viridis";
        trace.ShowScale = true;
        trace.Name = $"FastSurface {traceIndex}";
        trace.ShowLegend = false;

        return true;
    }

    /// <summary>Converts an internal coordinate to a finite data value, returning null for unrepresentable samples.</summary>
    private static float? GetScaledValue(double value, AxisScale scale)
    {
        if (scale == AxisScale.Logarithmic)
            value = Math.Pow(10.0, value);

        if (!Double.IsFinite(value) || value > Single.MaxValue || value < Single.MinValue)
            return null;

        return (float) value;
    }

    #endregion
}
