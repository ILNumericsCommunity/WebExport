namespace CSPlotlySlim.Chart.Generic;

/// <summary>Configures a legend heading and its optional subtitle.</summary>
public class LegendTitle : Title
{
    /// <summary>Gets or sets the secondary heading. A null value uses the Plotly default.</summary>
    public SubTitle? SubTitle { get; set; }

    /// <summary>Creates a heading whose text is the supplied string.</summary>
    /// <param name="text">The heading text.</param>
    public static implicit operator LegendTitle(string text) => new() { Text = text };
}
