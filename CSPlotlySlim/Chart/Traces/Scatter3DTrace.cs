using System.Collections.Generic;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Traces.Properties;

namespace CSPlotlySlim.Chart.Traces;

/// <summary>
/// Represents a 3D scatter trace for Plotly charts.
/// </summary>
/// <remarks>
/// Scatter3D traces display data points in 3D space. Points can be connected
/// with lines and/or displayed as markers depending on the <see cref="Mode" /> setting.
/// </remarks>
public class Scatter3DTrace : Trace
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
    public ScatterMode Mode { get; set; } = ScatterMode.Markers;

    /// <summary>
    /// Gets or sets the font for text labels.
    /// </summary>
    public Font? TextFont { get; set; }

    /// <inheritdoc />
    public override string Type => "scatter3d";

    /// <summary>
    /// Gets or sets the X coordinates of the data points.
    /// </summary>
    public IList<float>? X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinates of the data points.
    /// </summary>
    public IList<float>? Y { get; set; }

    /// <summary>
    /// Gets or sets the Z coordinates of the data points.
    /// </summary>
    public IList<float>? Z { get; set; }
}
