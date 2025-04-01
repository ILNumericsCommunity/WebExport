using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Layout.Properties;
using System.Drawing;

namespace CSPlotlySlim.Chart.Layout;

public class Legend
{
    public Color? BgColor { get; set; }

    public Color? BorderColor { get; set; }

    public int? BorderWidth { get; set; }

    public Font? Font { get; set; }

    public LegendTitle Title { get; set; }

    public float? X { get; set; }

    public XAnchor? XAnchor { get; set; }

    public float? Y { get; set; }

    public YAnchor? YAnchor { get; set; }
}