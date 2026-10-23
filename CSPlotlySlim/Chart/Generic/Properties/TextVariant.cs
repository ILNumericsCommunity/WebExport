using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

/// <summary>Specifies typographic variant options.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TextVariant
{
    /// <summary>Uses the normal typographic variant option.</summary>
    [JsonStringEnumMemberName("normal")]
    Normal,

    /// <summary>Uses the small caps typographic variant option.</summary>
    [JsonStringEnumMemberName("small-caps")]
    SmallCaps,

    /// <summary>Uses the all small caps typographic variant option.</summary>
    [JsonStringEnumMemberName("all-small-caps")]
    AllSmallCaps,

    /// <summary>Uses the all petite caps typographic variant option.</summary>
    [JsonStringEnumMemberName("all-petite-caps")]
    AllPetiteCaps,

    /// <summary>Uses the petite caps typographic variant option.</summary>
    [JsonStringEnumMemberName("petite-caps")]
    PetiteCaps,

    /// <summary>Uses the uni case typographic variant option.</summary>
    [JsonStringEnumMemberName("unicase")]
    UniCase
}
