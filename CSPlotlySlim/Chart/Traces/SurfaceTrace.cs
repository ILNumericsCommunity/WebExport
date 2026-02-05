using System.Collections.Generic;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Traces.Properties;

namespace CSPlotlySlim.Chart.Traces;

/// <summary>
/// Represents a 3D surface trace for Plotly charts.
/// </summary>
/// <remarks>
/// Surface traces visualize data as a 3D surface where the height (Z) is defined
/// over a grid of X and Y values. This is commonly used for topological visualizations,
/// mathematical functions, and terrain data.
/// </remarks>
public class SurfaceTrace : Trace
{
    /// <summary>
    /// Gets or sets the upper bound of the color domain.
    /// </summary>
    public float? CMax { get; set; }

    /// <summary>
    /// Gets or sets the lower bound of the color domain.
    /// </summary>
    public float? CMin { get; set; }

    /// <summary>
    /// Gets or sets the colorbar configuration.
    /// </summary>
    public Colorbar? Colorbar { get; set; }

    /// <summary>
    /// Gets or sets the colorscale for the surface.
    /// </summary>
    /// <remarks>
    /// Use predefined colorscales like "Viridis", "Plasma", "Blues", "RdBu", etc.
    /// Can also be a custom array of [value, color] pairs.
    /// </remarks>
    public string? Colorscale { get; set; }

    /// <summary>
    /// Gets or sets the contour configuration for the surface.
    /// </summary>
    public SurfaceContours? Contours { get; set; }

    /// <summary>
    /// Gets or sets hover information configuration.
    /// </summary>
    public string? HoverInfo { get; set; }

    /// <summary>
    /// Gets or sets the opacity of the surface (0.0 to 1.0).
    /// </summary>
    public float? Opacity { get; set; }

    /// <summary>
    /// Gets or sets whether to reverse the colorscale.
    /// </summary>
    public bool? ReversScale { get; set; }

    /// <summary>
    /// Gets or sets whether to show the color scale legend.
    /// </summary>
    public bool? ShowScale { get; set; }

    /// <inheritdoc />
    public override string Type => "surface";

    /// <summary>
    /// Gets or sets the X coordinates of the surface grid.
    /// </summary>
    /// <remarks>
    /// Should be a 1D array defining the X positions for each column of the Z matrix.
    /// If not provided, Plotly uses indices 0, 1, 2, ... as X values.
    /// </remarks>
    public IList<float>? X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinates of the surface grid.
    /// </summary>
    /// <remarks>
    /// Should be a 1D array defining the Y positions for each row of the Z matrix.
    /// If not provided, Plotly uses indices 0, 1, 2, ... as Y values.
    /// </remarks>
    public IList<float>? Y { get; set; }

    /// <summary>
    /// Gets or sets the Z values (heights) of the surface as a 2D matrix.
    /// </summary>
    /// <remarks>
    /// Z[i][j] represents the height at position (X[j], Y[i]).
    /// The outer list corresponds to rows (Y values), and the inner lists correspond to columns (X values).
    /// </remarks>
    public IList<IList<float?>>? Z { get; set; }
}
