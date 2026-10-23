using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSPlotlySlim.Chart.JSON;

// Plotly represents missing/non-finite samples as JSON null, preserving gaps in lines.
internal sealed class FiniteSingleJsonConverter : JsonConverter<float>
{
    public override float Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) => reader.TokenType == JsonTokenType.Null ? Single.NaN : reader.GetSingle();

    public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
    {
        if (Single.IsFinite(value))
            writer.WriteNumberValue(value);
        else
            writer.WriteNullValue();
    }
}

internal sealed class FiniteDoubleJsonConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) => reader.TokenType == JsonTokenType.Null ? Double.NaN : reader.GetDouble();

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        if (Double.IsFinite(value))
            writer.WriteNumberValue(value);
        else
            writer.WriteNullValue();
    }
}
