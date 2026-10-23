using System.Collections.Generic;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Traces;
using ILNumerics.Drawing;

namespace ILNumerics.Community.WebExport.Generator;

/// <summary>
/// Defines a contract for converting ILNumerics scene elements to Plotly traces.
/// </summary>
public interface IPlotBinder
{
    /// <summary>
    /// Binds an ILNumerics group element to Plotly traces and layout configuration.
    /// </summary>
    /// <param name="group">The ILNumerics group element to convert.</param>
    /// <param name="root">The root group (typically the PlotCube) containing the element.</param>
    /// <param name="layout">The Plotly layout to update with any layout-related properties.</param>
    /// <param name="traces">The list of traces to append newly created traces to.</param>
    void Bind(Group group, Group root, Layout layout, List<Trace> traces);
}
