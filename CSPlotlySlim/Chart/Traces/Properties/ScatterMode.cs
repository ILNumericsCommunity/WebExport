using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Traces.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ScatterMode
{
    [JsonStringEnumMemberName("lines")]
    Lines,
    [JsonStringEnumMemberName("markers")]
    Markers,
    [JsonStringEnumMemberName("lines+markers")]
    LinesMarkers,
    [JsonStringEnumMemberName("lines+markers+text")]
    LinesMarkersText,
    [JsonStringEnumMemberName("none")]
    None
}