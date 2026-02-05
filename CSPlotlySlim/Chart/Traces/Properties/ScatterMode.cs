using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Traces.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]

/// <summary>
/// Defines display modes for scatter traces.
/// </summary>
public enum ScatterMode
{
    /// <summary>
    /// Display lines connecting the data points.
    /// </summary>
    [JsonStringEnumMemberName("lines")]
    Lines,

    /// <summary>
    /// Display markers at each data point.
    /// </summary>
    [JsonStringEnumMemberName("markers")]
    Markers,

    /// <summary>
    /// Display both lines and markers.
    /// </summary>
    [JsonStringEnumMemberName("lines+markers")]
    LinesMarkers,

    /// <summary>
    /// Display lines, markers, and text labels.
    /// </summary>
    [JsonStringEnumMemberName("lines+markers+text")]
    LinesMarkersText,

    /// <summary>
    /// Display nothing for this trace.
    /// </summary>
    [JsonStringEnumMemberName("none")]
    None
}
