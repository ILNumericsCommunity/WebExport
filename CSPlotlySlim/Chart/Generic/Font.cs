using System.Drawing;
using CSPlotlySlim.Chart.Generic.Properties;

namespace CSPlotlySlim.Chart.Generic;

public class Font
{
    public Color? Color { get; set; }

    public string? Family { get; set; }

    public int? Size { get; set; }

    public FontStyle? Style { get; set; }

    public TextCase? TextCase { get; set; }

    public TextVariant? Variant { get; set; }

    public int? Weight { get; set; }
}