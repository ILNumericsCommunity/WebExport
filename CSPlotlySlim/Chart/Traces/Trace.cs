using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Traces;

[JsonDerivedType(typeof(ScatterTrace))]
[JsonDerivedType(typeof(Scatter3DTrace))]
[JsonDerivedType(typeof(SurfaceTrace))]
public abstract class Trace
{
    public abstract string Type { get; }

    public string? Name { get; set; }

    public bool? Visible { get; set; }

    public bool? ShowLegend { get; set; }
}