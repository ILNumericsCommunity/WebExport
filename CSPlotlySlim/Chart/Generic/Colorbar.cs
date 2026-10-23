using System.Drawing;

namespace CSPlotlySlim.Chart.Generic;

/// <summary>Configures the scale bar used to explain a trace color mapping.</summary>
public class Colorbar
{
    /// <summary>Gets or sets the background color. A null value uses the Plotly default.</summary>
    public Color? BgColor { get; set; }

    /// <summary>Gets or sets the border stroke color. A null value uses the Plotly default.</summary>
    public Color? BorderColor { get; set; }

    /// <summary>Gets or sets the border width in pixels. A null value uses the Plotly default.</summary>
    public int? BorderWidth { get; set; }

    /// <summary>Gets or sets the color bar thickness in pixels. A null value uses the Plotly default.</summary>
    public int? Thickness { get; set; }

    /// <summary>Gets or sets the heading configuration. A null value uses the Plotly default.</summary>
    public Title? Title { get; set; }

    //public Colorscale? Colorscale { get; set; }
}
