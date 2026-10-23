using System.Drawing;
using CSPlotlySlim.Chart.Generic.Properties;

namespace CSPlotlySlim.Chart.Generic;

/// <summary>Configures the symbols used to display individual data points.</summary>
public class Marker
{
    /// <summary>Gets or sets the whether Plotly chooses the color scale automatically. A null value uses the Plotly default.</summary>
    public bool? AutoColorScale { get; set; }

    /// <summary>Gets or sets the foreground color. A null value uses the Plotly default.</summary>
    public Color? Color { get; set; }

    /// <summary>Gets or sets the color scale legend configuration. A null value uses the Plotly default.</summary>
    public Colorbar? Colorbar { get; set; }

    /// <summary>Gets or sets the marker outline. A null value uses the Plotly default.</summary>
    public Line? Line { get; set; }

    /// <summary>Gets or sets the diameter in pixels. A null value uses the Plotly default.</summary>
    public int? Size { get; set; }

    /// <summary>Gets or sets the marker shape. A null value uses the Plotly default.</summary>
    public Symbol? Symbol { get; set; }
}
