using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

/// <summary>Specifies tick placement options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TickPosition
{
    /// <summary>Uses the outside tick placement option.</summary>
    [JsonStringEnumMemberName("outside")]
    Outside,

    /// <summary>Uses the inside tick placement option.</summary>
    [JsonStringEnumMemberName("inside")]
    Inside,

    /// <summary>Uses the none tick placement option.</summary>
    [JsonStringEnumMemberName("none")]
    None
}
