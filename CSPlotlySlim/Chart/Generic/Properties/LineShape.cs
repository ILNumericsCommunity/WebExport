using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

/// <summary>Specifies line interpolation options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LineShape
{
    /// <summary>Uses the linear line interpolation option.</summary>
    [JsonStringEnumMemberName("linear")]
    Linear,

    /// <summary>Uses the spline line interpolation option.</summary>
    [JsonStringEnumMemberName("spline")]
    Spline,

    /// <summary>Uses the hv line interpolation option.</summary>
    [JsonStringEnumMemberName("hv")]
    HV,

    /// <summary>Uses the vh line interpolation option.</summary>
    [JsonStringEnumMemberName("vh")]
    VH,

    /// <summary>Uses the hvh line interpolation option.</summary>
    [JsonStringEnumMemberName("hvh")]
    HVH,

    /// <summary>Uses the vhv line interpolation option.</summary>
    [JsonStringEnumMemberName("vhv")]
    VHV
}
