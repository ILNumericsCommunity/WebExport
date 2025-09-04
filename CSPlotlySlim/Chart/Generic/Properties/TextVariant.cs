using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.Generic.Properties;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TextVariant
{
    [JsonStringEnumMemberName("normal")]
    Normal,
    [JsonStringEnumMemberName("small-caps")]
    SmallCaps,
    [JsonStringEnumMemberName("all-small-caps")]
    AllSmallCaps,
    [JsonStringEnumMemberName("all-petite-caps")]
    AllPetiteCaps,
    [JsonStringEnumMemberName("petite-caps")]
    PetiteCaps,
    [JsonStringEnumMemberName("unicase")]
    UniCase
}