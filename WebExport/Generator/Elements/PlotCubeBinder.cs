using System;
using System.Collections.Generic;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Layout.AxesSubplots;
using CSPlotlySlim.Chart.Layout.Properties;
using CSPlotlySlim.Chart.Traces;
using ILNumerics.Drawing;
using Color = System.Drawing.Color;
using ILNLegend = ILNumerics.Drawing.Plotting.Legend;
using Legend = CSPlotlySlim.Chart.Layout.Legend;
using Title = ILNumerics.Drawing.Title;

namespace ILNumerics.Community.WebExport.Generator.Elements;

public class PlotCubeBinder : IPlotBinder
{
    public void Bind(Group group, Group root, Layout layout, List<Trace> traces)
    {
        if (!(group is PlotCube plotCube))
            return;

        // TODO: Font: Family, Size, Bold/Italic, Color
        layout.AutoSize = true;

        // Global
        var title = group.First<Title>();
        layout.Title = new CSPlotlySlim.Chart.Generic.Title
        {
            Text = title?.Label?.Text ?? String.Empty
        };
        layout.Title = title?.Label?.Text ?? String.Empty;

        // XAxis
        var xAxis = new XYZAxis
        {
            Title = plotCube.Axes.XAxis.Label.Text.ToMathTex(),
            Type = (plotCube.ScaleModes.XAxisScale == AxisScale.Linear) ? AxisType.Linear : AxisType.Log,
            AutoRange = false,
            Range = [plotCube.Axes.XAxis.Min ?? plotCube.Plots.Limits.XMin, plotCube.Axes.XAxis.Max ?? plotCube.Plots.Limits.XMax],
            ShowGrid = plotCube.Axes.XAxis.GridMajor.Visible,
            GridColor = plotCube.Axes.XAxis.GridMajor.Color ?? Color.FromArgb(230, 230, 230),
            GridWidth = plotCube.Axes.XAxis.GridMajor.Width,
            Mirror = AxisMirror.True
        };

        // YAxis
        var yAxis = new XYZAxis
        {
            Title = plotCube.Axes.YAxis.Label.Text.ToMathTex(),
            Type = (plotCube.ScaleModes.YAxisScale == AxisScale.Linear) ? AxisType.Linear : AxisType.Log,
            AutoRange = false,
            Range = [plotCube.Axes.YAxis.Min ?? plotCube.Plots.Limits.YMin, plotCube.Axes.YAxis.Max ?? plotCube.Plots.Limits.YMax],
            ShowGrid = plotCube.Axes.YAxis.GridMajor.Visible,
            GridColor = plotCube.Axes.YAxis.GridMajor.Color ?? Color.FromArgb(230, 230, 230),
            GridWidth = plotCube.Axes.YAxis.GridMajor.Width,
            Mirror = AxisMirror.True
        };

        if (plotCube.TwoDMode)
        {
            // 2D mode
            layout.XAxis = xAxis;
            layout.YAxis = yAxis;
        }
        else
        {
            // ZAxis
            var zAxis = new XYZAxis
            {
                Title = plotCube.Axes.ZAxis.Label.Text.ToMathTex(),
                Type = (plotCube.ScaleModes.ZAxisScale == AxisScale.Linear) ? AxisType.Linear : AxisType.Log,
                AutoRange = false,
                Range = [plotCube.Axes.ZAxis.Min ?? plotCube.Plots.Limits.ZMin, plotCube.Axes.ZAxis.Max ?? plotCube.Plots.Limits.ZMax],
                ShowGrid = plotCube.Axes.ZAxis.GridMajor.Visible,
                GridColor = plotCube.Axes.ZAxis.GridMajor.Color ?? Color.FromArgb(230, 230, 230),
                GridWidth = plotCube.Axes.ZAxis.GridMajor.Width,
                Mirror = AxisMirror.True
            };

            // 3D mode
            layout.Scene = new Scene3D()
            {
                XAxis = xAxis,
                YAxis = yAxis,
                ZAxis = zAxis
            };
        }

        // Legend
        var legend = plotCube.First<ILNLegend>();
        if (legend != null)
        {
            layout.ShowLegend = true;
            layout.Legend = new Legend()
            {
                BgColor = legend.Background.Color ?? Color.White,
                BorderColor = legend.Border.Color ?? Color.Black,
                BorderWidth = legend.Border.Width,
                X = legend.Location.X,
                Y = legend.Location.Y
            };
        }

        // Map plots (LinePlot, Surface, etc.)

        // LinePlots
        foreach (var linePlot in plotCube.Find<LinePlot>())
            linePlot.Bind<LinePlotBinder>(plotCube, layout, traces);

        //// SurfacePlots
        //foreach (var surface in plotCube.Find<ILNumerics.Drawing.Plotting.Surface>())
        //    surface.Bind<SurfaceBinder>(plotCube, traces, labels, layout);
    }
}
