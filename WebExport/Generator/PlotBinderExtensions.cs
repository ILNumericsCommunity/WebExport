using System.Collections.Generic;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Traces;
using ILNumerics.Drawing;

namespace ILNumerics.Community.WebExport.Generator;

public static class PlotBinderExtensions
{
    public static void Bind<TBinder>(this Group group, Group root, Layout layout, List<Trace> traces)
        where TBinder : IPlotBinder, new()
    {
        var binder = new TBinder();
        binder.Bind(group, root, layout, traces);
    }
}