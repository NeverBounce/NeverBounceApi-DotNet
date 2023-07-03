namespace NeverBounce.Utilities;

using Microsoft.Extensions.Logging;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using System.Collections;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static QueryStringUtility;

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
    Task<HttpContent> RequestGetContent(string endpoint, RequestModel? model = null);

    /// <summary>This method makes the HTTP request to the API and parses the response</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    /// <typeparam name="T">The expected response model to parse</typeparam>
    /// <returns>The parsed result, or null if no content.</returns>
    Task<T> RequestPost<T>(string endpoint, RequestModel model) where T : notnull, ResponseModel;
}

public sealed class NeverBounceHttpClient: INeverBounceHttpClient
{
    readonly string key;
    readonly IHttpServiceEndpoint endpointService;
    readonly ILogger? logger;

    /// <summary>Create an endpoint wrapper</summary>
    /// <param name="endpoint">The instance of the endpoint service to use to make requests</param>
    /// <param name="key">The api key to use to authenticate requests</param>
    /// <param name="loggerFactory">Optional logger</param>
    public NeverBounceHttpClient(IHttpServiceEndpoint endpoint, string key, ILoggerFactory? loggerFactory)
    {
        this.endpointService = endpoint;
        this.key = key;

        this.logger = loggerFactory?.CreateLogger<NeverBounceHttpClient>();
    }

    public async Task<HttpContent> RequestGetContent(string endpoint, RequestModel? model)
    {
        model ??= new RequestModel();
        model.Key = this.key;
        string getRequest = endpoint + "?" + ToQueryString(model);
        this.logger?.LogInformation("GET request to NeverBounce {EndPoint}", getRequest);
        var response = await this.endpointService.GetAsync(getRequest, CancellationToken.None);

        await EnsureResponseHasSuccessContent(response);

        return response.Content;
    }

    public async Task<T> RequestGet<T>(string endpoint, RequestModel? model) where T : notnull, ResponseModel
    {
        model ??= new RequestModel();
        model.Key = this.key;
        string getRequest = endpoint + "?" + ToQueryString(model);
        this.logger?.LogInformation("GET request to NeverBounce {EndPoint}", getRequest);

        var response = await this.endpointService.GetAsync(getRequest, CancellationToken.None);

        return await ParseResponse<T>(response);
    }

    public async Task<T> RequestPost<T>(string endpoint, RequestModel model) where T : notnull, ResponseModel
    {
        model.Key = this.key;
        string strContent = JsonUtility.Serialise(model);
        this.logger?.LogInformation("GET request to NeverBounce {EndPoint}, Content: {Content}", endpoint, strContent);

        var content = new StringContent(strContent, Encoding.UTF8, "application/json");
        var response = await this.endpointService.PostAsync(endpoint, content, CancellationToken.None);

        return await ParseResponse<T>(response);
    }

    /// <summary>Try and get the body from an error response.
    /// <para>As this is getting an error message it ignores any exception getting the message out</para></summary>
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

    static async Task<T> ParseResponse<T>(HttpResponseMessage response) where T : notnull, ResponseModel
    {
        await EnsureResponseHasSuccessContent(response);

        string? contentType = response.Content.Headers.ContentType?.ToString();
        string data = await response.Content.ReadAsStringAsync();

        // Expects "application/json", but may be "application/json; charset=utf-8"
        if (contentType is null || !contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
            // Handle unexpected response types
            throw new GeneralException($"""
                The response from NeverBounce was has a data type of "{contentType}", but "application/json" was expected. {response.StatusCode}
                {data}
                """);

        // Handle application/json responses
        var parsed = JsonUtility.Deserialise<T>(data);

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
                    We were unable to complete your request ({SnakeCase.Convert(status.ToString())})
                    {parsed.Message}
                    """),
            };

        // Return response data for passthrough/marshalling
        return parsed;
    }
}
