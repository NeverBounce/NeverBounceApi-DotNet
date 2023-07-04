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

    /// <summary>Serialise the given object and add the key as an extra property.
    /// <para>Applies snake_case formatting of properties and enums expected by NeverBounce.</para></summary>
    /// <typeparam name="T">The type to serialise.</typeparam>
    /// <param name="model">The instance to serialise.</param>
    /// <param name="key">The key to inject.</param>
    /// <returns>The serialised JSON string.</returns>
    public static string Serialise<T>(T model, string key) =>
        string.Concat("{\"key\":\"", key, "\",", JsonSerializer.Serialize<T>(model, JsonSettings).AsSpan(1));

    /// <summary>Parse JSON from the NeverBounce service, with snake_case property names.</summary>
    /// <typeparam name="T">The type to deserialise to.</typeparam>
    /// <param name="data">The JSON text data to parse</param>
    /// <returns>The result of a successful parse, or throws an exception. Never null.</returns>
    /// <exception cref="NeverBounceParseException">Thrown if JSON parsing throws an exception (see inner exception) or the result of the parse is null.</exception>
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
