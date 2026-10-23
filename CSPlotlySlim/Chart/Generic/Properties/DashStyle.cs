using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

/// <summary>Specifies line dash pattern options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DashStyle
{
    /// <summary>Uses the solid line dash pattern option.</summary>
    [JsonStringEnumMemberName("solid")]
    Solid,

    /// <summary>Uses the dot line dash pattern option.</summary>
    [JsonStringEnumMemberName("dot")]
    Dot,

    /// <summary>Uses the dash line dash pattern option.</summary>
    [JsonStringEnumMemberName("dash")]
    Dash,

    /// <summary>Uses the long dash line dash pattern option.</summary>
    [JsonStringEnumMemberName("longdash")]
    LongDash,

    /// <summary>Uses the dash dot line dash pattern option.</summary>
    [JsonStringEnumMemberName("dashdot")]
    DashDot,

    /// <summary>Uses the long dash dot line dash pattern option.</summary>
    [JsonStringEnumMemberName("longdashdot")]
    LongDashDot
}
