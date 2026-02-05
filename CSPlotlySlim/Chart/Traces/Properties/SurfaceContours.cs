namespace CSPlotlySlim.Chart.Traces.Properties;

/// <summary>
/// Represents contour configuration for 3D surface plots.
/// </summary>
public class SurfaceContours
{
    /// <summary>
    /// Gets or sets the X-plane contour configuration.
    /// </summary>
    public ContourProjection? X { get; set; }

    /// <summary>
    /// Gets or sets the Y-plane contour configuration.
    /// </summary>
    public ContourProjection? Y { get; set; }

    /// <summary>
    /// Gets or sets the Z-plane contour configuration.
    /// </summary>
    public ContourProjection? Z { get; set; }
}

/// <summary>
/// Represents a contour projection configuration for a single axis.
/// </summary>
public class ContourProjection
{
    /// <summary>
    /// Gets or sets whether to highlight contours on hover.
    /// </summary>
    public bool? Highlight { get; set; }

    /// <summary>
    /// Gets or sets the width of the highlighted contour lines.
    /// </summary>
    public int? HighlightWidth { get; set; }

    /// <summary>
    /// Gets or sets whether to project contours onto the axis plane.
    /// </summary>
    public bool? Project { get; set; }

    /// <summary>
    /// Gets or sets whether to show the contour lines.
    /// </summary>
    public bool? Show { get; set; }
}
