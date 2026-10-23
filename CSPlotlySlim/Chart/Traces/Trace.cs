using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Traces;

/// <summary>
/// Base class for all Plotly trace types.
/// </summary>
/// <remarks>
/// A trace defines the data and display style for a single series in a Plotly chart.
/// Derived types include <see cref="ScatterTrace" />, <see cref="Scatter3DTrace" />, and <see cref="SurfaceTrace" />.
/// </remarks>
[JsonDerivedType(typeof(ScatterTrace))]
[JsonDerivedType(typeof(Scatter3DTrace))]
[JsonDerivedType(typeof(SurfaceTrace))]
[JsonDerivedType(typeof(HeatmapTrace))]
[JsonDerivedType(typeof(Mesh3DTrace))]
public abstract class Trace
{
    /// <summary>
    /// Gets or sets the trace name displayed in the legend.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets whether to show this trace in the legend.
    /// </summary>
    public bool? ShowLegend { get; set; }

    /// <summary>
    /// Gets the trace type identifier used by Plotly.
    /// </summary>
    public abstract string Type { get; }

    /// <summary>
    /// Gets or sets whether the trace is visible.
    /// </summary>
    public bool? Visible { get; set; }
}
