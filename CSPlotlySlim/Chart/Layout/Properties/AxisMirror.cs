using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Layout.Properties;

/// <summary>Specifies axis mirroring options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AxisMirror
{
    /// <summary>Uses the true axis mirroring option.</summary>
    [JsonStringEnumMemberName("true")]
    True,

    /// <summary>Uses the ticks axis mirroring option.</summary>
    [JsonStringEnumMemberName("ticks")]
    Ticks,

    /// <summary>Uses the false axis mirroring option.</summary>
    [JsonStringEnumMemberName("false")]
    False,

    /// <summary>Uses the all axis mirroring option.</summary>
    [JsonStringEnumMemberName("all")]
    All,

    /// <summary>Uses the all ticks axis mirroring option.</summary>
    [JsonStringEnumMemberName("allticks")]
    AllTicks
}
