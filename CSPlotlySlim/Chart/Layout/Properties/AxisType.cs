using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AxisType
{
    [JsonStringEnumMemberName("-")]
    Auto,
    [JsonStringEnumMemberName("linear")]
    Linear,
    [JsonStringEnumMemberName("log")]
    Log,
    [JsonStringEnumMemberName("date")]
    Date,
    [JsonStringEnumMemberName("category")]
    Category,
    [JsonStringEnumMemberName("multicategory")]
    MultiCategory
}