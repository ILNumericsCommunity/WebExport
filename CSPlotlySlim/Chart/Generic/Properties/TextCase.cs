using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

/// <summary>Specifies text capitalization options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TextCase
{
    /// <summary>Uses the normal text capitalization option.</summary>
    [JsonStringEnumMemberName("normal")]
    Normal,

    /// <summary>Uses the word caps text capitalization option.</summary>
    [JsonStringEnumMemberName("word caps")]
    WordCaps,

    /// <summary>Uses the upper case text capitalization option.</summary>
    [JsonStringEnumMemberName("upper")]
    UpperCase,

    /// <summary>Uses the lower case text capitalization option.</summary>
    [JsonStringEnumMemberName("lower")]
    LowerCase
}
