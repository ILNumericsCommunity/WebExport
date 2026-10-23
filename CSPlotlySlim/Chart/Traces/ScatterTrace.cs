using System.Collections.Generic;
using System.Drawing;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Traces.Properties;

namespace CSPlotlySlim.Chart.Traces;

/// <summary>
/// Represents a 2D scatter trace for Plotly charts.
/// </summary>
/// <remarks>
/// Scatter traces display data points on a 2D plane. Points can be connected
/// with lines and/or displayed as markers depending on the <see cref="Mode" /> setting.
/// </remarks>
public class ScatterTrace : Trace
{
    /// <summary>
    /// Gets or sets the line styling options.
    /// </summary>
    public Line? Line { get; set; }

    /// <summary>
    /// Gets or sets the marker styling options.
    /// </summary>
    public Marker? Marker { get; set; }

    /// <summary>
    /// Gets or sets the display mode for the trace.
    /// </summary>
    public ScatterMode Mode { get; set; } = ScatterMode.LinesMarkers;

    /// <summary>
    /// Gets or sets the font for text labels.
    /// </summary>
    public Font? TextFont { get; set; }

    /// <summary>Optional Plotly fill mode, e.g. toself for closed polygons.</summary>
    public string? Fill { get; set; }

    /// <summary>Uniform fill color for polygons.</summary>
    public Color? FillColor { get; set; }

    /// <inheritdoc />
    public override string Type => "scatter";

    /// <summary>
    /// Gets or sets the X coordinates of the data points.
    /// </summary>
    public IList<float> X { get; set; } = [];

    /// <summary>
    /// Gets or sets the Y coordinates of the data points.
    /// </summary>
    public IList<float> Y { get; set; } = [];
}
