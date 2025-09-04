using System.Drawing;
using CSPlotlySlim.Chart.Generic.Properties;

namespace CSPlotlySlim.Chart.Generic;

public class Marker
{
    public bool? AutoColorScale { get; set; }

    public Color? Color { get; set; }

    public Colorbar? Colorbar { get; set; }

    public int? Size { get; set; }

    public Symbol? Symbol { get; set; }
}