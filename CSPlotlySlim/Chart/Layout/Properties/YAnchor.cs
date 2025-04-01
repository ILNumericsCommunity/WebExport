using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum YAnchor
{
    [JsonStringEnumMemberName("auto")]
    Auto,
    [JsonStringEnumMemberName("top")]
    Top,
    [JsonStringEnumMemberName("middle")]
    Middle,
    [JsonStringEnumMemberName("bottom")]
    Bottom,
}