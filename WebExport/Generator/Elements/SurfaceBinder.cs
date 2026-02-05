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

    private static string? ResolveColorscale(object? colormap)
    {
        if (colormap == null)
            return null;

        var name = colormap.GetType().Name;
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

    private static bool TryCreateFromSurface(Surface surface, ScaleModes scaleModes, int traceIndex, out SurfaceTrace trace)
    {
        trace = new SurfaceTrace();

        Array<float> positions = surface.Positions; // n x n x 3 (z, x, y)
        if (isnull(positions))
            return false;

        int rows;
        int cols;
        int depth;
        try
        {
            rows = (int) positions.S[0];
            cols = (int) positions.S[1];
            depth = (int) positions.S[2];
        }
        catch (Exception)
        {
            return false;
        }

        if (rows <= 0 || cols <= 0 || depth < 3)
            return false;

        try
        {
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
                    var z = GetScaledValue(positions.GetValue(i, j, 0), scaleModes.ZAxisScale);
                    zRow.Add(z);
                }
                zMatrix.Add(zRow);
            }
            trace.Z = zMatrix;
        }
        catch (Exception)
        {
            return false;
        }

        var colorscale = ResolveColorscale(surface.Colormap);
        trace.Colorscale = colorscale ?? "Viridis";
        trace.ShowScale = true;
        trace.Name = $"Surface {traceIndex}";
        trace.ShowLegend = false;

        return true;
    }

    private static bool TryCreateFromFastSurface(FastSurface fastSurface, ScaleModes scaleModes, int traceIndex, out SurfaceTrace trace)
    {
        trace = new SurfaceTrace();

        Array<float> positions = fastSurface.Fill.Positions.Storage; // 3 (z, x, y) x n or 3 x rows x cols
        if (isnull(positions))
            return false;

        try
        {
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
                    var x = GetScaledValue(positions.GetValue(1, 0, j), scaleModes.XAxisScale);
                    if (!x.HasValue)
                        return false;
                    xValues.Add(x.Value);
                }

                var yValues = new List<float>(rows);
                for (var i = 0; i < rows; i++)
                {
                    var y = GetScaledValue(positions.GetValue(2, i, 0), scaleModes.YAxisScale);
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
                        var z = GetScaledValue(positions.GetValue(0, i, j), scaleModes.ZAxisScale);
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
                var zMatrix = new List<IList<float?>>();

                for (var i = 0; i < count; i++)
                {
                    var x = GetScaledValue(positions.GetValue(1, i), scaleModes.XAxisScale);
                    var y = GetScaledValue(positions.GetValue(2, i), scaleModes.YAxisScale);
                    var z = GetScaledValue(positions.GetValue(0, i), scaleModes.ZAxisScale);

                    if (!x.HasValue || !y.HasValue)
                        continue;

                    if (!xIndex.TryGetValue(x.Value, out var xi))
                    {
                        xi = xValues.Count;
                        xValues.Add(x.Value);
                        xIndex.Add(x.Value, xi);

                        foreach (var row in zMatrix)
                            row.Add(null);
                    }

                    if (!yIndex.TryGetValue(y.Value, out var yi))
                    {
                        yi = yValues.Count;
                        yValues.Add(y.Value);
                        yIndex.Add(y.Value, yi);

                        var newRow = new List<float?>(xValues.Count);
                        for (var j = 0; j < xValues.Count; j++)
                            newRow.Add(null);
                        zMatrix.Add(newRow);
                    }

                    zMatrix[yi][xi] = z;
                }

                if (xValues.Count == 0 || yValues.Count == 0)
                    return false;

                trace.X = xValues;
                trace.Y = yValues;
                trace.Z = zMatrix;
            }
        }
        catch (Exception)
        {
            return false;
        }

        var colorscale = ResolveColorscale(fastSurface.Colormap);
        trace.Colorscale = colorscale ?? "Viridis";
        trace.ShowScale = true;
        trace.Name = $"FastSurface {traceIndex}";
        trace.ShowLegend = false;

        return true;
    }

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
