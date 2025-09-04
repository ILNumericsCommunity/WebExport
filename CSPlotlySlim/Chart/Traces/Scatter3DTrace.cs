using System.Collections.Generic;
using CSPlotlySlim.Chart.Generic;
using CSPlotlySlim.Chart.Traces.Properties;

namespace CSPlotlySlim.Chart.Traces;

public class Scatter3DTrace : Trace
{
    public override string Type => "scatter3d";

    public ScatterMode Mode { get; set; }

    public IList<float> X { get; set; }

    public IList<float> Y { get; set; }

    public IList<float> Z { get; set; }

    public Marker Marker { get; set; }

    public Font TextFont { get; set; }
}