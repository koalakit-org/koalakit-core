using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Kamel.Primitives.Extensions;

namespace Kamel.Primitives.Serializations;

public static class AppSerializer
{
    private static readonly JsonSerializerOptions DefaultSerializeOptions = new()
    {
        WriteIndented = true,
        IncludeFields = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };

    private static readonly JsonSerializerOptions DefaultDeserializeOptions = new()
    {
        IncludeFields = true,
        PropertyNameCaseInsensitive = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };

    public static string ToJson<T>(T? obj, JsonSerializerOptions? options = null)
    {
        if (obj == null)
        {
            return string.Empty;
        }

        options ??= DefaultSerializeOptions;
        return JsonSerializer.Serialize(obj, options);
    }

    public static byte[] ToBinary<T>(T? obj)
    {
        var json = ToJson(obj);
        if (json == null)
        {
            return [];
        }

        return Encoding.UTF8.GetBytes(json);
    }

    public static byte[] ToBinary(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return [];
        }

        return Encoding.UTF8.GetBytes(text);
    }

    public static T? Deserialize<T>(string? json, JsonSerializerOptions? options = null)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }

        options ??= DefaultDeserializeOptions;
        return JsonSerializer.Deserialize<T>(json, options);
    }

    public static T? Deserialize<T>(byte[]? bytes)
    {
        if (bytes.HasItems())
        {
            return Deserialize<T>(Encoding.UTF8.GetString(bytes));
        }
        return default;
    }

    public static bool IsValidJson(string value)
    {
        try
        {
            JsonDocument.Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }
}