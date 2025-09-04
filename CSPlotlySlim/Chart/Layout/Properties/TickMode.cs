using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TickMode
{
    [JsonStringEnumMemberName("auto")]
    Auto,
    [JsonStringEnumMemberName("linear")]
    Linear,
    [JsonStringEnumMemberName("array")]
    Array
}