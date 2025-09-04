using System.Drawing;
using CSPlotlySlim.Chart.Generic.Properties;

namespace CSPlotlySlim.Chart.Generic;

public class Line
{
    public Color? Color { get; set; }

    public DashStyle? Dash { get; set; }

    public LineShape? Shape { get; set; }

    public int? Width { get; set; }
}