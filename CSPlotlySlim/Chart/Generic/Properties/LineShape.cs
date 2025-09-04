using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LineShape
{
    [JsonStringEnumMemberName("linear")]
    Linear,
    [JsonStringEnumMemberName("spline")]
    Spline,
    [JsonStringEnumMemberName("hv")]
    HV,
    [JsonStringEnumMemberName("vh")]
    VH,
    [JsonStringEnumMemberName("hvh")]
    HVH,
    [JsonStringEnumMemberName("vhv")]
    VHV
}