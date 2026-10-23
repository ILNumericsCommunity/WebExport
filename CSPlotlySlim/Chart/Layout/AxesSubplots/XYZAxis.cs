using System.Drawing;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Generic.Properties;
using CSPlotlySlim.Chart.Layout.Properties;

namespace CSPlotlySlim.Chart.Layout.AxesSubplots;

/// <summary>Configures a Cartesian axis in a two-dimensional chart or a three-dimensional scene.</summary>
public class XYZAxis
{
    /// <summary>Gets or sets the whether bounds are inferred from the trace data. A null value uses the Plotly default.</summary>
    public bool? AutoRange { get; set; }

    /// <summary>Gets or sets the foreground color. A null value uses the Plotly default.</summary>
    public Color? Color { get; set; }

    /// <summary>Gets or sets the minor tick and grid configuration. A null value uses the Plotly default.</summary>
    public MinorAxis? Minor { get; set; }

    /// <summary>Gets or sets the axis line and tick mirroring mode. A null value uses the Plotly default.</summary>
    public AxisMirror? Mirror { get; set; }

    /// <summary>Gets or sets the two axis endpoints; logarithmic axes use base-ten exponents. A null value uses the Plotly default.</summary>
    public float[]? Range { get; set; }

    /// <summary>Gets or sets the side on which the axis is drawn. A null value uses the Plotly default.</summary>
    public AxisSide? Side { get; set; }

    /// <summary>Gets or sets the heading configuration. A null value uses the Plotly default.</summary>
    public Title? Title { get; set; }

    /// <summary>Gets or sets the axis scale and data interpretation. A null value uses the Plotly default.</summary>
    public AxisType? Type { get; set; }

    #region Line

    /// <summary>Gets or sets the axis stroke color. A null value uses the Plotly default.</summary>
    public Color? LineColor { get; set; }

    /// <summary>Gets or sets the axis stroke width in pixels. A null value uses the Plotly default.</summary>
    public int? LineWidth { get; set; }

    /// <summary>Gets or sets the whether the axis stroke is visible. A null value uses the Plotly default.</summary>
    public bool? ShowLine { get; set; }

    #endregion

    #region Grid

    /// <summary>Gets or sets the grid stroke color. A null value uses the Plotly default.</summary>
    public Color? GridColor { get; set; }

    /// <summary>Gets or sets the grid dash pattern. A null value uses the Plotly default.</summary>
    public DashStyle? GridDash { get; set; }

    /// <summary>Gets or sets the grid stroke width in pixels. A null value uses the Plotly default.</summary>
    public int? GridWidth { get; set; }

    /// <summary>Gets or sets the whether grid lines are visible. A null value uses the Plotly default.</summary>
    public bool? ShowGrid { get; set; }

    #endregion

    //#region Spike

    //public Color? SpikeColor { get; set; }

    //public DashStyle? SpikeDash { get; set; }

    //public int? SpikeThickness { get; set; }

    //public bool? ShowSpikes { get; set; }

    //#endregion

    #region Ticks

    /// <summary>Gets or sets the tick stroke color. A null value uses the Plotly default.</summary>
    public Color? TickColor { get; set; }

    /// <summary>Gets or sets the tick label font. A null value uses the Plotly default.</summary>
    public Font? TickFont { get; set; }

    /// <summary>Gets or sets the Plotly numeric or date tick formatting string. A null value uses the Plotly default.</summary>
    public string? TickFormat { get; set; }

    /// <summary>Gets or sets the tick length in pixels. A null value uses the Plotly default.</summary>
    public int? TickLen { get; set; }

    /// <summary>Gets or sets the tick generation mode. A null value uses the Plotly default.</summary>
    public TickMode? TickMode { get; set; }

    /// <summary>Gets or sets the tick position relative to the axis. A null value uses the Plotly default.</summary>
    public TickPosition? Ticks { get; set; }

    /// <summary>Gets or sets the maximum automatic tick count. A null value uses the Plotly default.</summary>
    public int? NTicks { get; set; }

    /// <summary>Gets or sets the labels corresponding to explicit tick coordinates. A null value uses the Plotly default.</summary>
    public string[]? TickText { get; set; }

    /// <summary>Gets or sets the explicit tick coordinates. A null value uses the Plotly default.</summary>
    public float[]? TickVals { get; set; }

    /// <summary>Gets or sets the tick stroke width in pixels. A null value uses the Plotly default.</summary>
    public int? TickWidth { get; set; }

    /// <summary>Gets or sets the whether tick labels are visible. A null value uses the Plotly default.</summary>
    public bool? ShowTickLabels { get; set; }

    #endregion
}
