using System.Drawing;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Layout.Properties;

namespace CSPlotlySlim.Chart.Layout.Layers;

public class Annotations
{
    public Color? BgColor { get; set; }

    public Color? BorderColor { get; set; }

    public int? BorderWidth { get; set; }

    public Font? Font { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

    public string[]? Text { get; set; }

    public float[]? X { get; set; }

    public XAnchor[]? XAnchor { get; set; }

    public float[]? Y { get; set; }

    public YAnchor[]? YAnchor { get; set; }
}