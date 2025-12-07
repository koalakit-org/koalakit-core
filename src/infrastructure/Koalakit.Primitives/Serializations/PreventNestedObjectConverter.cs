using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kamel.Primitives.Serializations;

public sealed class PreventNestedObjectConverter : JsonConverter<object>
{
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize(ref reader, typeToConvert, options) ?? throw new Exception("PreventNestedObjectConverter");
    }

    public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else if (value is string || value.GetType().IsPrimitive || value is DateTime || value is Guid)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
        else
        {
            writer.WriteStringValue("[Nested object]");
        }
    }
}