using CSPlotlySlim.Chart.Generic;
using System.Drawing;
using CSPlotlySlim.Chart.Layout.AxesSubplots;

namespace CSPlotlySlim.Chart.Layout;

public class Layout
{
    public Title? Title { get; set; }

    public bool? ShowLegend { get; set; }

    public Legend? Legend { get; set; }

    public bool? AutoSize { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public Font? Font { get; set; }

    public Color? Paper_BgColor { get; set; }

    public Color? Plot_BgColor { get; set; }

    //  TODO: Grid support
    
    public XYZAxis? XAxis { get; set; }

    public XYZAxis? YAxis { get; set; }

    public Scene3D? Scene { get; set; }
}