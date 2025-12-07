using System.Text.Json;
using System.Text.Json.Serialization;
using Kamel.Primitives.DateTimes;

namespace Kamel.Primitives.Serializations;

public class LocalDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var dateString = reader.GetString();
            if (DateTime.TryParse(dateString, out var dateTime))
            {
                return dateTime;
            }
        }

        throw new JsonException($"Unable to parse DateTime from {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Always write as string without timezone info for absolute values
        writer.WriteStringValue(value.ToAbsoluteString());
    }
}