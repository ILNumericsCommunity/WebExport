using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AxisSide
{
    [JsonStringEnumMemberName("top")]
    Top,
    [JsonStringEnumMemberName("bottom")]
    Bottom,
    [JsonStringEnumMemberName("left")]
    Left,
    [JsonStringEnumMemberName("right")]
    Right
}