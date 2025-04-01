using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DashStyle
{
    [JsonStringEnumMemberName("solid")]
    Solid,
    [JsonStringEnumMemberName("dot")]
    Dot,
    [JsonStringEnumMemberName("dash")]
    Dash,
    [JsonStringEnumMemberName("longdash")]
    LongDash,
    [JsonStringEnumMemberName("dashdot")]
    DashDot,
    [JsonStringEnumMemberName("longdashdot")]
    LongDashDot
}