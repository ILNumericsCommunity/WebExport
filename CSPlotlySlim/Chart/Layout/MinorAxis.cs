using CSPlotlySlim.Chart.Generic.Properties;
using System.Drawing;
using CSPlotlySlim.Chart.Layout.Properties;

namespace CSPlotlySlim.Chart.Layout;

public class MinorAxis
{
    public Color? GridColor { get; set; }

    public DashStyle? GridDash { get; set; }

    public int? GridWidth { get; set; }

    public int? NTicks { get; set; }

    public bool? ShowGrid { get; set; }

    public Color? TickColor { get; set; }

    public int? TickLen { get; set; }

    public TickMode? TickMode { get; set; }

    public TickPosition? Ticks { get; set; }

    public float[] TickVals { get; set; }

    public int? TickWidth { get; set; }
}