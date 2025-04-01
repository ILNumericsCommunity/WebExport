using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TickPosition
{
    [JsonStringEnumMemberName("outside")]
    Outside,
    [JsonStringEnumMemberName("inside")]
    Inside,
    [JsonStringEnumMemberName("none")]
    None
}