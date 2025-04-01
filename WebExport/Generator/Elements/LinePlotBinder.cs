using System;
using System.Collections.Generic;
using System.Linq;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Traces;
using CSPlotlySlim.Chart.Traces.Properties;
using ILNumerics.Drawing;
using Color = System.Drawing.Color;
using ILNLegend = ILNumerics.Drawing.Plotting.Legend;
using Marker = CSPlotlySlim.Chart.Generic.Marker;

namespace ILNumerics.Community.WebExport.Generator.Elements;

public class LinePlotBinder : IPlotBinder
{
    #region IPlotBinder Members

    public void Bind(Group group, Group root, Layout layout, List<Trace> traces)
    {
        if (!(group is LinePlot linePlot))
            return;

        if (!(root is PlotCube plotCube))
            return;

        var trace = new ScatterTrace();

        // Trace data
        Array<float> xValues = squeeze(linePlot.Positions[0, full]);
        Array<float> yValues = squeeze(linePlot.Positions[1, full]);
        if (plotCube.ScaleModes.XAxisScale == AxisScale.Logarithmic)
            xValues = pow(10f, xValues);
        if (plotCube.ScaleModes.YAxisScale == AxisScale.Logarithmic)
            yValues = pow(10f, yValues);
        trace.X = xValues.ToArray();
        trace.Y = yValues.ToArray();

        var hasMarker = (linePlot.Marker.Style != MarkerStyle.None && linePlot.Marker.Visible);
        var hasLine = (linePlot.Line.Width > 0 && linePlot.Line.Visible);
        if (hasMarker && hasLine)
            trace.Mode = ScatterMode.LinesMarkers;
        else if (hasMarker)
            trace.Mode = ScatterMode.Markers;
        else if (hasLine)
            trace.Mode = ScatterMode.Lines;

        // Marker
        trace.Marker = new Marker
        {
            Color = linePlot.Marker.Fill.Color ?? linePlot.Line.Color ?? Color.Black,
            Size = Math.Max(linePlot.Marker.Size / 2, 1),
            Symbol = linePlot.Marker.Style.ToPlotlyMarkerSymbol()
        };

        // Line
        trace.Line = new CSPlotlySlim.Chart.Generic.Line
        {
            Width = linePlot.Line.Width,
            Color = linePlot.Line.Color ?? Color.Black,
            Dash = linePlot.Line.DashStyle.ToPlotlyDashStyle()
        };

        // Legend
        var name = $"Line {traces.Count}";
        var legend = plotCube.First<ILNLegend>();
        if (legend != null)
        {
            var legendItems = legend.Find<LegendItem>().ToArray();
            var legendItem = legendItems.FirstOrDefault(item => item.ProviderID == linePlot.ID);
            if (legendItem != null)
                trace.Name = legendItem.Text.ToMathTex();
            else
            {
                var idx = Array.IndexOf(plotCube.Find<LinePlot>().ToArray(), linePlot);
                if (idx != -1 && idx < legendItems.Length)
                    legendItem = legendItems[idx];
                trace.Name = legendItem?.Text.ToMathTex() ?? name;
            }

            trace.ShowLegend = true;
        }

        traces.Add(trace);
    }

    #endregion
}
