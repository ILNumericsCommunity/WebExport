using System;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.JSON;

/// <summary>Serializes drawing colors as CSS RGBA strings without losing alpha precision.</summary>
public class ColorJsonConverter : JsonConverter<Color>
{
    /// <inheritdoc />
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var text = reader.GetString() ?? throw new JsonException("A color must be a string.");
        if (text.StartsWith("rgba(", StringComparison.OrdinalIgnoreCase) && text.EndsWith(')'))
        {
            var parts = text[5..^1].Split(',');
            if (parts.Length == 4
                && Byte.TryParse(parts[0], out var red)
                && Byte.TryParse(parts[1], out var green)
                && Byte.TryParse(parts[2], out var blue)
                && Double.TryParse(parts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out var alpha)
                && Double.IsFinite(alpha) && alpha >= 0 && alpha <= 1)
                return Color.FromArgb((int) Math.Round(alpha * 255), red, green, blue);
            throw new JsonException("Invalid RGBA color.");
        }
        try
        {
            return ColorTranslator.FromHtml(text);
        }
        catch (ArgumentException error)
        {
            throw new JsonException("Invalid HTML color.", error);
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(FormattableString.Invariant($"rgba({value.R},{value.G},{value.B},{value.A / 255f:R})"));
    }
}
