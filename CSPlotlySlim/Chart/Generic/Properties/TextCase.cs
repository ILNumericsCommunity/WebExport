using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TextCase
{
    [JsonStringEnumMemberName("normal")]
    Normal,
    [JsonStringEnumMemberName("word caps")]
    WordCaps,
    [JsonStringEnumMemberName("upper")]
    UpperCase,
    [JsonStringEnumMemberName("lower")]
    LowerCase
}