using System.Collections.Generic;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Traces.Properties;

namespace CSPlotlySlim.Chart.Traces;

public class ScatterTrace : Trace
{
    public override string Type => "scatter";

    public ScatterMode Mode { get; set; } = ScatterMode.LinesMarkers;

    public IList<float> X { get; set; } = [];

    public IList<float> Y { get; set; } = [];

    public Marker? Marker { get; set; }

    public Line? Line { get; set; }

    public Font? TextFont { get; set; }
}