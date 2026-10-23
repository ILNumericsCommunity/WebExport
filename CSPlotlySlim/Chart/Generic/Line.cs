using System.Drawing;
using CSPlotlySlim.Chart.Generic.Properties;

namespace CSPlotlySlim.Chart.Generic;

/// <summary>Configures the stroke used to connect trace points.</summary>
public class Line
{
    /// <summary>Gets or sets the foreground color. A null value uses the Plotly default.</summary>
    public Color? Color { get; set; }

    /// <summary>Gets or sets the stroke dash pattern. A null value uses the Plotly default.</summary>
    public DashStyle? Dash { get; set; }

    /// <summary>Gets or sets the interpolation used between data points. A null value uses the Plotly default.</summary>
    public LineShape? Shape { get; set; }

    /// <summary>Gets or sets the width in pixels. A null value uses the Plotly default.</summary>
    public int? Width { get; set; }
}
