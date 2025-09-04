using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AxisMirror
{
    [JsonStringEnumMemberName("true")]
    True,
    [JsonStringEnumMemberName("ticks")]
    Ticks,
    [JsonStringEnumMemberName("false")]
    False,
    [JsonStringEnumMemberName("all")]
    All,
    [JsonStringEnumMemberName("allticks")]
    AllTicks
}