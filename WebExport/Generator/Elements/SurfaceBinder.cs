using System;
using System.Collections.Generic;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Traces;
using ILNumerics.Drawing;
using Surface = ILNumerics.Drawing.Plotting.Surface;

namespace ILNumerics.Community.WebExport.Generator.Elements;

public class SurfaceBinder : IPlotBinder
{
    public void Bind(Group group, Group root, Layout layout, List<Trace> traces)
    {
        if (!(group is Surface surface))
            return;

        // TODO: Colormap, etc.
    }

    #region FormatHelper

    private static IEnumerable<string> DataTable(Surface surface)
    {
        var scaleModes = surface.FirstUp<PlotCubeDataGroup>().ScaleModes;

        yield return "  table[row sep=crcr]{";

        Array<float> positions = surface.Positions; // n x n x 3 (z, x, y)
        for (var i = 0; i < positions.S[0]; i++)
        {
            for (var j = 0; j < positions.S[1]; j++)
            {
                Array<float> xyz = positions[i, j, full];
                var x = (float) xyz[1];
                if (scaleModes.XAxisScale == AxisScale.Logarithmic)
                    x = (float) Math.Pow(10.0, x);
                var y = (float) xyz[2];
                if (scaleModes.YAxisScale == AxisScale.Logarithmic)
                    y = (float) Math.Pow(10.0, y);
                var z = (float) xyz[0];
                if (scaleModes.ZAxisScale == AxisScale.Logarithmic)
                    z = (float) Math.Pow(10.0, z);

                yield return FormattableString.Invariant($"  {x}  {y} {z}\\\\");
            }
        }

        yield return "};";
    }

    #endregion
}
