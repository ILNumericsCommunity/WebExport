using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

/// <summary>Specifies tick generation options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TickMode
{
    /// <summary>Uses the auto tick generation option.</summary>
    [JsonStringEnumMemberName("auto")]
    Auto,

    /// <summary>Uses the linear tick generation option.</summary>
    [JsonStringEnumMemberName("linear")]
    Linear,

    /// <summary>Uses the array tick generation option.</summary>
    [JsonStringEnumMemberName("array")]
    Array
}
