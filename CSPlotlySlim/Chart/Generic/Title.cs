namespace CSPlotlySlim.Chart.Generic;

/// <summary>Represents a chart or axis heading with optional font settings.</summary>
public class Title
{
    /// <summary>Gets or sets the font configuration. A null value uses the Plotly default.</summary>
    public Font? Font { get; set; }

    /// <summary>Gets or sets the displayed text, including supported Plotly markup. A null value uses the Plotly default.</summary>
    public required string Text { get; set; }

    /// <summary>Creates a heading whose text is the supplied string.</summary>
    /// <param name="text">The heading text.</param>
    public static implicit operator Title(string text) => new() { Text = text };
}
