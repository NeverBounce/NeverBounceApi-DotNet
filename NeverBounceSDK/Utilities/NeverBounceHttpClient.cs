namespace NeverBounce.Utilities; 
using NeverBounce.Exceptions;
using NeverBounce.Models;
using System.Collections;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public interface INeverBounceHttpClient {

    /// <summary>This method makes the HTTP request to the API and parses the response</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    /// <typeparam name="T">The expected response model to parse</typeparam>
    /// <returns>The parsed result, or null if no content.</returns>
    Task<T> RequestGet<T>(string endpoint, RequestModel? model = null) where T: notnull, ResponseModel;

    /// <summary>This method makes the HTTP request to the API</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    Task<string?> RequestGetBody(string endpoint, RequestModel? model = null);

    /// <summary>This method makes the HTTP request to the API and parses the response</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    /// <typeparam name="T">The expected response model to parse</typeparam>
    /// <returns>The parsed result, or null if no content.</returns>
    Task<T> RequestPost<T>(string endpoint, RequestModel model) where T : notnull, ResponseModel;
}

public sealed class NeverBounceHttpClient: INeverBounceHttpClient
{
    readonly NeverBounceSettings settings;

    readonly IHttpClient _client;

    //private readonly string _host = "https://api.neverbounce.com/v4/";

    string acceptedType = "application/json";

    /// <summary>NeverBounce API JSON serialisation settings, use snake_case for properties and enums</summary>
    static JsonSerializerOptions JsonSettings { get; }

    static NeverBounceHttpClient()
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


    /// <summary>
    ///     Creates the HttpClient instance as well as sets up the hostname to use
    /// </summary>
    /// <param name="Client">The instance of IHttpClient to use to make requests</param>
    /// <param name="ApiKey">The api key to use to authenticate requests</param>
    /// <param name="Host">The url to make the API request to</param>
    public NeverBounceHttpClient(IHttpClient Client, NeverBounceSettings settings)
    {
        this._client = Client;
        this.settings = settings;

        this.setUserAgent();
    }

    /// <summary>
    ///     This will set the expected data type for the response. If a different
    ///     data type is return an error will be thrown.
    /// </summary>
    /// <param name="type">The expected request data type</param>
    public void SetAcceptedType(string type)
    {
        this.acceptedType = type;
    }

    /// <summary>Sets the user agent on the request</summary>
    void setUserAgent()
    {
        string userAgent = GenerateUserAgent();
        this._client.DefaultRequestHeaders.Remove("User-Agent");
        this._client.DefaultRequestHeaders.Add("User-Agent", userAgent);
    }

    /// <summary>Generates the useragent string</summary>
    static string GenerateUserAgent()
    {
        string productVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0";
        return "NeverBounce/" + productVersion;
    }

    public async Task<string?> RequestGetBody(string endpoint, RequestModel? model)
    {
        model ??= new RequestModel();

        model.Key = this.settings.Key;

        var uri = new Uri(this.settings.Url + endpoint + "?" + ToQueryString(model));
        var response = await this._client.GetAsync(uri);

        await EnsureResponseHasSuccessContent(response);
        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<T> RequestGet<T>(string endpoint, RequestModel? model) where T : notnull, ResponseModel
    {
        model ??= new RequestModel();

        model.Key = this.settings.Key;

        var uri = new Uri(this.settings.Url + endpoint + "?" + ToQueryString(model));
        var response = await this._client.GetAsync(uri);

        return await this.ParseResponse<T>(response);
    }

    public async Task<T> RequestPost<T>(string endpoint, RequestModel model) where T : notnull, ResponseModel
    {
        model.Key = this.settings.Key;

        var uri = new Uri(this.settings.Url + endpoint);
        var content = new StringContent(JsonSerializer.Serialize(model, JsonSettings), Encoding.UTF8, "application/json");
        var response = await this._client.PostAsync(uri, content);

        return await this.ParseResponse<T>(response);
    }

    static async Task<string> ResponseBodyForce(HttpResponseMessage response) {
        try { return "Body: " + await response.Content.ReadAsStringAsync(); }
        catch (Exception x) { return $"Unable to read body: {x.Message}"; }
    }

    static async Task EnsureResponseHasSuccessContent(HttpResponseMessage response) {
        // Handle 5xx HTTP errors
        if (response.StatusCode.GetHashCode() > 500)
            throw new GeneralException($"""
                Error on server: {response.StatusCode}
                {await ResponseBodyForce(response)}
                """);

        // Handle 4xx HTTP errors
        if (response.StatusCode == HttpStatusCode.RequestEntityTooLarge)
            throw new GeneralException($"""
                Entity too large: {response.StatusCode}
                This API enforces a max request size of 25 Megabytes.
                {await ResponseBodyForce(response)}
                """);


        // Handle 4xx HTTP errors
        if (response.StatusCode.GetHashCode() > 400)
            throw new GeneralException($"""
                Bad request: {response.StatusCode}
                {await ResponseBodyForce(response)}
                """);

        if (!response.IsSuccessStatusCode)
            throw new GeneralException($"""
                Error response code: {response.StatusCode}
                {await ResponseBodyForce(response)}
                """);

        if (response.StatusCode == HttpStatusCode.NoContent)
            throw new GeneralException("Success no content (204)");
    }

    async Task<T> ParseResponse<T>(HttpResponseMessage response) where T : notnull, ResponseModel
    {
        await EnsureResponseHasSuccessContent(response);

        string? contentType = response.Content.Headers.ContentType?.ToString();
        string data = await response.Content.ReadAsStringAsync();

        if (contentType != "application/json")
            // Handle unexpected response types
            throw new GeneralException($"""
                The response from NeverBounce was has a data type of "{contentType}", but "application/json" was expected. {response.StatusCode}
                {data}
                """);

        // Handle application/json responses
        T? parsed;

        try { parsed = JsonSerializer.Deserialize<T>(data, JsonSettings); }
        catch (Exception x)
        {
            throw new GeneralException($"""
                The response from NeverBounce was unable to be parsed as JSON.
                Try the request again, if this error persists let us know at support@neverbounce.com
                Parse error: {x.Message} 
                Response body: 
                {data}
                """);
        }

        if(parsed is null)
            throw new GeneralException($"""
                The response from NeverBounce was unable to be parsed as JSON.
                Try the request again, if this error persists let us know at support@neverbounce.com
                Response body: 
                {data}
                """);

        // Handle non 'success' statuses that return with HTTP success codes but an error message in the body
        var status = parsed.Status;
        if (status != ResponseStatus.Success)
            throw status switch
            {
                // "auth_failure":
                ResponseStatus.AuthFailure => new AuthException($"""
                    We were unable to authenticate your request (auth_failure)
                    {parsed.Message}
                    """),
                // "temp_unavail":
                ResponseStatus.TempUnavail => new GeneralException($"""
                    We were unable to complete your request (temp_unavail)
                    {parsed.Message}
                    """),
                // "throttle_triggered":
                ResponseStatus.ThrottleTriggered => new ThrottleException($"""
                    We were unable to complete your request (throttle_triggered)
                    {parsed.Message}
                    """),
                // "bad_referrer":
                ResponseStatus.BadReferrer => new BadReferrerException($"""
                    We were unable to complete your request (bad_referrer)
                    {parsed.Message}
                    """),
                _ => new GeneralException($"""
                    We were unable to complete your request ({status})
                    {parsed.Message}
                    """),
            };

        // Return response data for passthrough/marshalling
        return parsed;
    }

    /// <summary>Creates a urlencoded query string of the parameters</summary>
    /// <param name="request">The request parameters to encode</param>
    /// <param name="parentProperty">The nested parent parameter</param>
    public static string ToQueryString(object request, string? parentProperty = null)
    {
        // Get all properties on the object
        var properties = request.GetType().GetProperties()
            .Where(x => x.CanRead)
            .Where(x => x.GetValue(request, null) is not null)
            .ToDictionary(
                x => x.Name,
                x => x.PropertyType.ToString().Contains("System.Boolean")
                    ? Convert.ToInt32(x.GetValue(request, null))
                    : x.GetValue(request, null)
            );

        // Get names for all IEnumerable properties (excl. string)
        var propertyNames = properties
            .Where(x => x.Value is not string && x.Value is IEnumerable)
            .Select(x => x.Key)
            .ToList();
        
        // Concat all IEnumerable properties into a comma separated string
        foreach (string? key in propertyNames)
        {
            object? value = properties[key];
            if (value is null) continue;

            var valueType = value.GetType();
            var valueElemType = valueType.IsGenericType
                ? valueType.GetGenericArguments()[0]
                : valueType.GetElementType();

            if (valueElemType is null) continue;
            
            if (valueElemType.IsPrimitive || valueElemType == typeof(string))
            {
                var enumerable = value as IEnumerable;
                if(enumerable is not null)
                    properties[key] = string.Join(",", enumerable.Cast<object>());
            }
        }

        // Concat all key/value pairs into a string separated by ampersand
        return string.Join("&", properties
            .Select(x => BuildQueryStringKeyValue(x, parentProperty)));
    }
    
    /// <summary>Builds URI safe key value pair</summary>
    /// <param name="pair">The key value pair to encode</param>
    /// <param name="parentProperty">The nested parent parameter</param>
    static string? BuildQueryStringKeyValue(KeyValuePair<string, object?> pair, string? parentProperty = null)
    {
        if(pair.Value is null) return null;

        string key = parentProperty is not null
            ? $"{parentProperty}[{Uri.EscapeDataString(pair.Key)}]"
            : Uri.EscapeDataString(pair.Key);

        if (pair.Value.GetType().IsPrimitive || pair.Value.GetType().IsValueType || pair.Value is string) {
            string? valueStr = pair.Value.ToString();
            if (valueStr is not null)
                return $"{key}={Uri.EscapeDataString(valueStr)}";
            else
                return null;
        }

        return ToQueryString(pair.Value, key);
    }
}
