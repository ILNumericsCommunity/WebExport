using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FontStyle
{
    [JsonStringEnumMemberName("normal")]
    Normal,
    [JsonStringEnumMemberName("italic")]
    Italic,
    [JsonStringEnumMemberName("bold")]
    Bold
}