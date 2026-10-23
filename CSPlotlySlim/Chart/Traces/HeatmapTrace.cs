using System.Collections.Generic;

namespace CSPlotlySlim.Chart.Traces;

/// <summary>A rectangular scalar image with explicit cell centers and color limits.</summary>
public sealed class HeatmapTrace : Trace
{
    /// <inheritdoc />
    public override string Type => "heatmap";

    /// <summary>Column center coordinates.</summary>
    public IList<float> X { get; set; } = [];

    /// <summary>Row center coordinates.</summary>
    public IList<float> Y { get; set; } = [];

    /// <summary>Row-major scalar values; null represents a missing cell.</summary>
    public IList<IList<float?>> Z { get; set; } = [];

    /// <summary>Lower color-domain bound.</summary>
    public float? ZMin { get; set; }

    /// <summary>Upper color-domain bound.</summary>
    public float? ZMax { get; set; }

    /// <summary>Normalized position and RGBA color pairs.</summary>
    public IList<object[]> Colorscale { get; set; } = [];

    /// <summary>Whether to show the color scale.</summary>
    public bool ShowScale { get; set; } = true;

    /// <summary>Missing cells are never interpolated.</summary>
    public bool ConnectGaps => false;
}
