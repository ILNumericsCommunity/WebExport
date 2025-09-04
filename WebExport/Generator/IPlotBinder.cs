using System.Collections.Generic;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Traces;
using ILNumerics.Drawing;

namespace ILNumerics.Community.WebExport.Generator;

public interface IPlotBinder
{
    void Bind(Group group, Group root, Layout layout, List<Trace> traces);
}