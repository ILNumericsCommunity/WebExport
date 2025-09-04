using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum XAnchor
{
    [JsonStringEnumMemberName("auto")]
    Auto,
    [JsonStringEnumMemberName("left")]
    Left,
    [JsonStringEnumMemberName("center")]
    Center,
    [JsonStringEnumMemberName("right")]
    Right,
}