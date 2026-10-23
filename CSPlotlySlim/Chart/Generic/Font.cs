using System.Drawing;
using CSPlotlySlim.Chart.Generic.Properties;

namespace CSPlotlySlim.Chart.Generic;

/// <summary>Configures the typeface and appearance of Plotly text.</summary>
public class Font
{
    /// <summary>Gets or sets the foreground color. A null value uses the Plotly default.</summary>
    public Color? Color { get; set; }

    /// <summary>Gets or sets the CSS font family list. A null value uses the Plotly default.</summary>
    public string? Family { get; set; }

    /// <summary>Gets or sets the size in pixels. A null value uses the Plotly default.</summary>
    public int? Size { get; set; }

    /// <summary>Gets or sets the font style. A null value uses the Plotly default.</summary>
    public FontStyle? Style { get; set; }

    /// <summary>Gets or sets the text capitalization mode. A null value uses the Plotly default.</summary>
    public TextCase? TextCase { get; set; }

    /// <summary>Gets or sets the typographic capitalization variant. A null value uses the Plotly default.</summary>
    public TextVariant? Variant { get; set; }

    /// <summary>Gets or sets the numeric font weight. A null value uses the Plotly default.</summary>
    public int? Weight { get; set; }
}
