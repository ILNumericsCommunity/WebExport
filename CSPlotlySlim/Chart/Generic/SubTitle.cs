using CSPlotlySlim.Chart.Layout.Properties;

namespace CSPlotlySlim.Chart.Generic;

/// <summary>Represents a secondary heading with positioning and font settings.</summary>
public class SubTitle
{
    /// <summary>Gets or sets the font configuration. A null value uses the Plotly default.</summary>
    public Font? Font { get; set; }

    /// <summary>Gets or sets the displayed text, including supported Plotly markup. A null value uses the Plotly default.</summary>
    public required string Text { get; set; }

    /// <summary>Gets or sets the horizontal position. A null value uses the Plotly default.</summary>
    public float? X { get; set; }

    /// <summary>Gets or sets the horizontal anchor at the configured position. A null value uses the Plotly default.</summary>
    public XAnchor? XAnchor { get; set; }

    /// <summary>Gets or sets the vertical position. A null value uses the Plotly default.</summary>
    public float? Y { get; set; }

    /// <summary>Gets or sets the vertical anchor at the configured position. A null value uses the Plotly default.</summary>
    public YAnchor? YAnchor { get; set; }

    /// <summary>Creates a heading whose text is the supplied string.</summary>
    /// <param name="text">The heading text.</param>
    public static implicit operator SubTitle(string text) => new() { Text = text };
}
