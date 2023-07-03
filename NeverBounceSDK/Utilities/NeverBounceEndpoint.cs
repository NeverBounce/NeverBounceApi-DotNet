namespace NeverBounce.Utilities;
using Microsoft.Extensions.Logging;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using System.Net;
using System.Text;
using static QueryStringUtility;

public sealed class NeverBounceEndpoint: INeverBounceEndpoint
{
    readonly string key;
    readonly IHttpServiceEndpoint endpointService;
    readonly ILogger? logger;

    /// <summary>Create an endpoint wrapper</summary>
    /// <param name="endpoint">The instance of the endpoint service to use to make requests</param>
    /// <param name="key">The api key to use to authenticate requests</param>
    /// <param name="loggerFactory">Optional logger</param>
    public NeverBounceEndpoint(IHttpServiceEndpoint endpoint, string key, ILoggerFactory? loggerFactory)
    {
        this.endpointService = endpoint;
        this.key = key;

        this.logger = loggerFactory?.CreateLogger<NeverBounceEndpoint>();
    }

    public async Task<HttpContent> RequestGetContent(string endpoint, object? model)
    {
        string getRequest =  $"{endpoint}?key={Uri.EscapeDataString(this.key)}";
        if (model is not null) getRequest += "&" + ToQueryString(model);

        this.logger?.LogInformation("GET request to NeverBounce {EndPoint}", getRequest);
        var response = await this.endpointService.GetAsync(getRequest, CancellationToken.None);

        await EnsureResponseHasSuccessContent(response);

        return response.Content;
    }

    public async Task<T> RequestGet<T>(string endpoint, object? model) where T : notnull, ResponseModel
    {
        string getRequest = $"{endpoint}?key={Uri.EscapeDataString(this.key)}";
        if (model is not null) getRequest += "&" + ToQueryString(model);

        this.logger?.LogInformation("GET request to NeverBounce {EndPoint}", getRequest);

        var response = await this.endpointService.GetAsync(getRequest, CancellationToken.None);

        return await ParseResponse<T>(response);
    }

    public async Task<T> RequestPost<T>(string endpoint, object model) where T : notnull, ResponseModel
    {
        string strContent = JsonUtility.Serialise(model, this.key);
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
        if ((int) response.StatusCode > 500)
            throw new NeverBounceResponseException(response.StatusCode, $"""
                Error on server: {response.StatusCode}
                {await ResponseBodyForce(response)}
                """);

        // Handle 4xx HTTP errors
        if (response.StatusCode == HttpStatusCode.RequestEntityTooLarge)
            throw new NeverBounceResponseException(response.StatusCode, $"""
                Entity too large: {response.StatusCode}
                This API enforces a max request size of 25 Megabytes.
                {await ResponseBodyForce(response)}
                """);

        // Handle 4xx HTTP errors
        if ((int) response.StatusCode > 400)
            throw new NeverBounceResponseException(response.StatusCode, $"""
                Bad request: {response.StatusCode}
                {await ResponseBodyForce(response)}
                """);

        if (!response.IsSuccessStatusCode)
            throw new NeverBounceResponseException(response.StatusCode, $"""
                Error response code: {response.StatusCode}
                {await ResponseBodyForce(response)}
                """);

        if (response.StatusCode == HttpStatusCode.NoContent)
            throw new NeverBounceResponseException(HttpStatusCode.NoContent, "Success no content (204)");
    }

    static async Task<T> ParseResponse<T>(HttpResponseMessage response) where T : notnull, ResponseModel
    {
        await EnsureResponseHasSuccessContent(response);

        string data = await response.Content.ReadAsStringAsync();
        if(string.IsNullOrWhiteSpace(data))
            throw new NeverBounceResponseException(response.StatusCode, "No message content");

        // Expects "application/json", but may be "application/json; charset=utf-8"
        string? contentType = response.Content.Headers.ContentType?.ToString();
        if (contentType is null || !contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
            // Handle unexpected response types
            throw new NeverBounceResponseException(response.StatusCode, $"""
                The response from NeverBounce was has a data type of "{contentType}", but "application/json" was expected. {response.StatusCode}
                {data}
                """);

        // Handle application/json responses
        var parsed = JsonUtility.Deserialise<T>(data);

        // Handle non 'success' statuses that return with HTTP success codes but an error message in the body
        var status = parsed.Status;
        if (status != ResponseStatus.Success)
            throw new NeverBounceServiceException(status, $"""
                {(status == ResponseStatus.AuthFailure ?
                    "We were unable to authenticate your request" :
                    "We were unable to complete your request ")} ({SnakeCase.Convert(status.ToString())})
                {parsed.Message}
                """);

        // Return response data for passthrough/marshalling
        return parsed;
    }
}
