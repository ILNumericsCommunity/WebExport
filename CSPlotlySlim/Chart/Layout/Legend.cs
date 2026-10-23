using System.Drawing;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Layout.Properties;

namespace CSPlotlySlim.Chart.Layout;

/// <summary>Configures the appearance and position of a chart legend.</summary>
public class Legend
{
    /// <summary>Gets or sets the background color. A null value uses the Plotly default.</summary>
    public Color? BgColor { get; set; }

    /// <summary>Gets or sets the border stroke color. A null value uses the Plotly default.</summary>
    public Color? BorderColor { get; set; }

    /// <summary>Gets or sets the border width in pixels. A null value uses the Plotly default.</summary>
    public int? BorderWidth { get; set; }

    /// <summary>Gets or sets the font configuration. A null value uses the Plotly default.</summary>
    public Font? Font { get; set; }

    /// <summary>Gets or sets the heading configuration. A null value uses the Plotly default.</summary>
    public LegendTitle? Title { get; set; }

    /// <summary>Gets or sets the horizontal position. A null value uses the Plotly default.</summary>
    public float? X { get; set; }

    /// <summary>Gets or sets the horizontal anchor at the configured position. A null value uses the Plotly default.</summary>
    public XAnchor? XAnchor { get; set; }

    /// <summary>Gets or sets the vertical position. A null value uses the Plotly default.</summary>
    public float? Y { get; set; }

    /// <summary>Gets or sets the vertical anchor at the configured position. A null value uses the Plotly default.</summary>
    public YAnchor? YAnchor { get; set; }
}
