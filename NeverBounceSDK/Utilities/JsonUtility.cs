namespace NeverBounce.Utilities;
using NeverBounce.Exceptions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

static class JsonUtility
{
    /// <summary>NeverBounce API JSON serialisation settings, use snake_case for properties and enums</summary>
    static JsonSerializerOptions JsonSettings { get; }

    static JsonUtility()
    {
        var namingPolicy = new SnakeCase();
        JsonSettings = new JsonSerializerOptions
        {
            DictionaryKeyPolicy = namingPolicy,
            PropertyNamingPolicy = namingPolicy,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };

        JsonSettings.Converters.Add(new JsonStringEnumConverter(namingPolicy));
    }

    public static string Serialise<T>(T model) =>
        JsonSerializer.Serialize<T>(model, JsonSettings);

    public static T Deserialise<T>(string data) {
        T? parsed;
        try { parsed = JsonSerializer.Deserialize<T>(data, JsonSettings); }
        catch (Exception x)
        {
            throw new NeverBounceParseException($"""
                The response from NeverBounce was unable to be parsed as JSON.
                Try the request again, if this error persists let us know at support@neverbounce.com
                Parse error: {x.Message} 
                Response body: 
                {data}
                """, x);
        }

        if (parsed is null)
            throw new NeverBounceParseException($"""
                The response from NeverBounce was unable to be parsed as JSON.
                Try the request again, if this error persists let us know at support@neverbounce.com
                Response body: 
                {data}
                """);

        return parsed;
    }
}
