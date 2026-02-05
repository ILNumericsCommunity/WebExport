using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]

/// <summary>
/// Defines vertical anchor positions for layout elements.
/// </summary>
public enum YAnchor
{
    /// <summary>
    /// Automatically determines the vertical anchor position.
    /// </summary>
    [JsonStringEnumMemberName("auto")]
    Auto,

    /// <summary>
    /// Anchors the element to the top.
    /// </summary>
    [JsonStringEnumMemberName("top")]
    Top,

    /// <summary>
    /// Anchors the element to the vertical middle.
    /// </summary>
    [JsonStringEnumMemberName("middle")]
    Middle,

    /// <summary>
    /// Anchors the element to the bottom.
    /// </summary>
    [JsonStringEnumMemberName("bottom")]
    Bottom
}
