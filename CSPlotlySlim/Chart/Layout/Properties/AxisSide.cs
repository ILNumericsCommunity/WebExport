using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

/// <summary>Specifies axis placement options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AxisSide
{
    /// <summary>Uses the top axis placement option.</summary>
    [JsonStringEnumMemberName("top")]
    Top,

    /// <summary>Uses the bottom axis placement option.</summary>
    [JsonStringEnumMemberName("bottom")]
    Bottom,

    /// <summary>Uses the left axis placement option.</summary>
    [JsonStringEnumMemberName("left")]
    Left,

    /// <summary>Uses the right axis placement option.</summary>
    [JsonStringEnumMemberName("right")]
    Right
}
