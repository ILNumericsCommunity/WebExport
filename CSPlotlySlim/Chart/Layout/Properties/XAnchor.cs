using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]

/// <summary>
/// Defines horizontal anchor positions for layout elements.
/// </summary>
public enum XAnchor
{
    /// <summary>
    /// Automatically determines the horizontal anchor position.
    /// </summary>
    [JsonStringEnumMemberName("auto")]
    Auto,

    /// <summary>
    /// Anchors the element to the left.
    /// </summary>
    [JsonStringEnumMemberName("left")]
    Left,

    /// <summary>
    /// Anchors the element to the horizontal center.
    /// </summary>
    [JsonStringEnumMemberName("center")]
    Center,

    /// <summary>
    /// Anchors the element to the right.
    /// </summary>
    [JsonStringEnumMemberName("right")]
    Right
}
