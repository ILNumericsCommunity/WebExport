using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

/// <summary>Specifies font styling options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FontStyle
{
    /// <summary>Uses the normal font styling option.</summary>
    [JsonStringEnumMemberName("normal")]
    Normal,

    /// <summary>Uses the italic font styling option.</summary>
    [JsonStringEnumMemberName("italic")]
    Italic,

    /// <summary>Uses the bold font styling option.</summary>
    [JsonStringEnumMemberName("bold")]
    Bold
}
