using System.Drawing;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Generic.Properties;
using CSPlotlySlim.Chart.Layout.Properties;

namespace CSPlotlySlim.Chart.Layout.AxesSubplots;

public class XYZAxis
{
    public Color? Color { get; set; }

    public MinorAxis? Minor { get; set; }

    public AxisMirror? Mirror { get; set; }

    public bool? AutoRange { get; set; }

    public float[]? Range { get; set; }

    public AxisSide? Side { get; set; }

    public Title? Title { get; set; }

    public AxisType? Type { get; set; }

    #region Line

    public Color? LineColor { get; set; }

    public int? LineWidth { get; set; }

    public bool? ShowLine { get; set; }

    #endregion

    #region Grid

    public Color? GridColor { get; set; }

    public DashStyle? GridDash { get; set; }

    public int? GridWidth { get; set; }

    public bool? ShowGrid { get; set; }

    #endregion

    //#region Spike

    //public Color? SpikeColor { get; set; }

    //public DashStyle? SpikeDash { get; set; }

    //public int? SpikeThickness { get; set; }

    //public bool? ShowSpikes { get; set; }

    //#endregion

    #region Ticks

    public Color? TickColor { get; set; }

    public Font? TickFont { get; set; }

    public string? TickFormat { get; set; }

    public int? TickLen { get; set; }

    public TickMode? TickMode { get; set; }

    public TickPosition? Ticks { get; set; }

    public int? NTicks { get; set; }

    public string[] TickText { get; set; }

    public float[] TickVals { get; set; }

    public int? TickWidth { get; set; }

    public bool? ShowTickLabels { get; set; }

    #endregion
}