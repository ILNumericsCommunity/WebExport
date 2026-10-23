using System.Drawing;
using CSPlotlySlim.Chart.Generic.Properties;
using CSPlotlySlim.Chart.Layout.Properties;

namespace CSPlotlySlim.Chart.Layout;

/// <summary>Configures minor tick marks and grid lines.</summary>
public class MinorAxis
{
    /// <summary>Gets or sets the grid stroke color. A null value uses the Plotly default.</summary>
    public Color? GridColor { get; set; }

    /// <summary>Gets or sets the grid dash pattern. A null value uses the Plotly default.</summary>
    public DashStyle? GridDash { get; set; }

    /// <summary>Gets or sets the grid stroke width in pixels. A null value uses the Plotly default.</summary>
    public int? GridWidth { get; set; }

    /// <summary>Gets or sets the maximum automatic tick count. A null value uses the Plotly default.</summary>
    public int? NTicks { get; set; }

    /// <summary>Gets or sets the whether grid lines are visible. A null value uses the Plotly default.</summary>
    public bool? ShowGrid { get; set; }

    /// <summary>Gets or sets the tick stroke color. A null value uses the Plotly default.</summary>
    public Color? TickColor { get; set; }

    /// <summary>Gets or sets the tick length in pixels. A null value uses the Plotly default.</summary>
    public int? TickLen { get; set; }

    /// <summary>Gets or sets the tick generation mode. A null value uses the Plotly default.</summary>
    public TickMode? TickMode { get; set; }

    /// <summary>Gets or sets the tick position relative to the axis. A null value uses the Plotly default.</summary>
    public TickPosition? Ticks { get; set; }

    /// <summary>Gets or sets the explicit tick coordinates. A null value uses the Plotly default.</summary>
    public float[]? TickVals { get; set; }

    /// <summary>Gets or sets the tick stroke width in pixels. A null value uses the Plotly default.</summary>
    public int? TickWidth { get; set; }
}
