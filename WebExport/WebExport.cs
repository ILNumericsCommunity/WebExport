using System;
using System.Collections.Generic;
using CSPlotlySlim.Chart;
using CSPlotlySlim.Chart.Layout;
using ILNumerics.Community.WebExport.Generator;
using ILNumerics.Community.WebExport.Generator.Elements;
using ILNumerics.Drawing;
using Trace = CSPlotlySlim.Chart.Traces.Trace;

namespace ILNumerics.Community.WebExport;

public static class WebExport
{
    public static Chart? Export(Scene scene)
    {
        if (scene == null)
            throw new ArgumentNullException(nameof(scene));

        var layout = new Layout();
        var traces = new List<Trace>();

        var plotCube = scene.First<PlotCube>();
        if (plotCube != null)
            plotCube.Bind<PlotCubeBinder>(plotCube, layout, traces);

        // Failed to bind anything -> return null
        if (traces.Count == 0)
            return null;

        return new Chart(layout, traces.ToArray());
    }
}