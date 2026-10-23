using System.Drawing;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Layout.AxesSubplots;

namespace CSPlotlySlim.Chart.Layout;

/// <summary>Configures chart dimensions, axes, titles, backgrounds, and legends.</summary>
public class Layout
{
    /// <summary>Gets or sets the whether dimensions are inferred from the chart container. A null value uses the Plotly default.</summary>
    public bool? AutoSize { get; set; }

    /// <summary>Gets or sets the font configuration. A null value uses the Plotly default.</summary>
    public Font? Font { get; set; }

    /// <summary>Gets or sets the height in pixels. A null value uses the Plotly default.</summary>
    public int? Height { get; set; }

    /// <summary>Gets or sets the legend configuration. A null value uses the Plotly default.</summary>
    public Legend? Legend { get; set; }

    /// <summary>Gets or sets the background color outside the plotting area. A null value uses the Plotly default.</summary>
    public Color? Paper_BgColor { get; set; }

    /// <summary>Gets or sets the background color inside the plotting area. A null value uses the Plotly default.</summary>
    public Color? Plot_BgColor { get; set; }

    /// <summary>Gets or sets the three-dimensional scene configuration. A null value uses the Plotly default.</summary>
    public Scene3D? Scene { get; set; }

    /// <summary>Gets or sets the whether the chart legend is visible. A null value uses the Plotly default.</summary>
    public bool? ShowLegend { get; set; }

    /// <summary>Gets or sets the heading configuration. A null value uses the Plotly default.</summary>
    public Title? Title { get; set; }

    /// <summary>Gets or sets the width in pixels. A null value uses the Plotly default.</summary>
    public int? Width { get; set; }

    //  TODO: Grid support

    /// <summary>Gets or sets the X-axis configuration. A null value uses the Plotly default.</summary>
    public XYZAxis? XAxis { get; set; }

    /// <summary>Gets or sets the Y-axis configuration. A null value uses the Plotly default.</summary>
    public XYZAxis? YAxis { get; set; }
}
