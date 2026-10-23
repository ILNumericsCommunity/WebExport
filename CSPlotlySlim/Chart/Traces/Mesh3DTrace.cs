using System.Collections.Generic;

namespace CSPlotlySlim.Chart.Traces;

/// <summary>Explicit triangle geometry without re-triangulation by Plotly.</summary>
public sealed class Mesh3DTrace : Trace
{
    /// <inheritdoc />
    public override string Type => "mesh3d";

    /// <summary>Vertex X coordinates.</summary>
    public IList<float> X { get; set; } = [];

    /// <summary>Vertex Y coordinates.</summary>
    public IList<float> Y { get; set; } = [];

    /// <summary>Vertex Z coordinates.</summary>
    public IList<float> Z { get; set; } = [];

    /// <summary>First vertex index of each triangle.</summary>
    public IList<int> I { get; set; } = [];

    /// <summary>Second vertex index of each triangle.</summary>
    public IList<int> J { get; set; } = [];

    /// <summary>Third vertex index of each triangle.</summary>
    public IList<int> K { get; set; } = [];

    /// <summary>RGBA colors for individual triangles.</summary>
    public IList<string> FaceColor { get; set; } = [];

    /// <summary>Preserve individual triangle normals.</summary>
    public bool FlatShading => true;
}
