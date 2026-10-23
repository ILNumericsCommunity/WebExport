using System.Collections.Generic;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Traces;
using ILNumerics.Drawing;

namespace ILNumerics.Community.WebExport.Generator;

/// <summary>Provides strongly typed dispatch to Plotly scene binders.</summary>
public static class PlotBinderExtensions
{
    /// <summary>Creates the requested binder and appends converted plot data to the supplied trace collection.</summary>
    public static void Bind<TBinder>(this Group group, Group root, Layout layout, List<Trace> traces)
        where TBinder : IPlotBinder, new()
    {
        var binder = new TBinder();
        binder.Bind(group, root, layout, traces);
    }
}
