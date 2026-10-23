using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

/// <summary>Specifies axis scale options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AxisType
{
    /// <summary>Uses the auto axis scale option.</summary>
    [JsonStringEnumMemberName("-")]
    Auto,

    /// <summary>Uses the linear axis scale option.</summary>
    [JsonStringEnumMemberName("linear")]
    Linear,

    /// <summary>Uses the log axis scale option.</summary>
    [JsonStringEnumMemberName("log")]
    Log,

    /// <summary>Uses the date axis scale option.</summary>
    [JsonStringEnumMemberName("date")]
    Date,

    /// <summary>Uses the category axis scale option.</summary>
    [JsonStringEnumMemberName("category")]
    Category,

    /// <summary>Uses the multi category axis scale option.</summary>
    [JsonStringEnumMemberName("multicategory")]
    MultiCategory
}
