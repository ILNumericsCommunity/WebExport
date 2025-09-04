using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.JSON;

public class ColorJsonConverter : JsonConverter<Color>
{
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ColorTranslator.FromHtml(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(FormattableString.Invariant($"rgba({value.R},{value.G},{value.B},{value.A / 255f:F1})"));
    }
}