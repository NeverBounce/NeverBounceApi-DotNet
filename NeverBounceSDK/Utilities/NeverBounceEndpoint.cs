namespace NeverBounce.Utilities;
using Microsoft.Extensions.Logging;
using NeverBounce.Exceptions;
using NeverBounce.Models;
using System.Net;
using System.Text;
using static QueryStringUtility;

sealed class NeverBounceEndpoint: INeverBounceEndpoint
{
    const int MAX_POST_BYTES = 25 * 1024 * 1024;
    const int MAX_LOG_BYTES = 1024;
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

    async Task<HttpContent> RequestGetContentRaw(string endpoint, object? model, CancellationToken? cancellationToken)
    {
        string getRequest = $"{endpoint}?key={Uri.EscapeDataString(this.key)}";
        if (model is not null) getRequest += "&" + ToQueryString(model);

        this.logger?.LogInformation("GET request to NeverBounce {EndPoint}", getRequest);
        var response = await this.endpointService.GetAsync(getRequest, cancellationToken ?? CancellationToken.None);
        await EnsureResponseHasSuccessContent(response);

        return response.Content;
    }


    public async Task<HttpContent> RequestGetContent(string endpoint, object? model, CancellationToken? cancellationToken)
    {
        var content = await this.RequestGetContentRaw(endpoint, model, cancellationToken);

        string? contentType = content.Headers.ContentType?.ToString();
        if (contentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase) ?? false) {
            // We're expecting a stream and got JSON instead, usually this will be an error message
            string data = await content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(data))
                throw new NeverBounceResponseException(HttpStatusCode.OK, "No message content");

            this.logger?.LogWarning("Unexpected response: {JSON}", data);

            // Parse the result for errors
            CheckServiceStatus(JsonUtility.Deserialise<ResponseModel>(data));
        }

        return content;
    }

    public async Task<T> RequestGet<T>(string endpoint, object? model, CancellationToken? cancellationToken) 
        where T : notnull, ResponseModel => 
        await ParseResponse<T>(await this.RequestGetContentRaw(endpoint, model, cancellationToken));

    public async Task<T> RequestPost<T>(string endpoint, object model, CancellationToken? cancellationToken) 
        where T : notnull, ResponseModel
    {
        string strContent = JsonUtility.Serialise(model, this.key);

        // Show a warning if we think we're over the limit
        int byteCount = Encoding.UTF8.GetByteCount(strContent);
        if(byteCount > MAX_POST_BYTES)
            this.logger?.LogWarning("POST request to NeverBounce {EndPoint}, Size: {Size} exceeds 25MB limit", endpoint, ByteSize(byteCount));
        else if (byteCount > MAX_LOG_BYTES)
            this.logger?.LogInformation("POST request to NeverBounce {EndPoint}, Size: {Size}", endpoint, ByteSize(byteCount));
        else
            this.logger?.LogInformation("POST request to NeverBounce {EndPoint}, Content: {Content}", endpoint, strContent);

        var content = new StringContent(strContent, Encoding.UTF8, "application/json");
        var response = await this.endpointService.PostAsync(endpoint, content, cancellationToken ?? CancellationToken.None);
        await EnsureResponseHasSuccessContent(response);

        return await ParseResponse<T>(response.Content);
    }

    /// <summary>Try and get the body from an error response.
    /// <para>As this is getting an error message it ignores any exception getting the message out</para></summary>
    static async Task<string> ResponseBodyForce(HttpResponseMessage response) {
        try { return "Body: " + await response.Content.ReadAsStringAsync(); }
        catch (Exception x) { return $"Unable to read body: {x.Message}"; }
    }

    /// <summary>Check the HTTP status code and throw exception if not valid.</summary>
    /// <exception cref="NeverBounceResponseException">Thrown if the status code indicates an error.</exception>
    static async Task EnsureResponseHasSuccessContent(HttpResponseMessage response) {
        // Handle 5xx HTTP errors
        if ((int) response.StatusCode > 500)
            throw new NeverBounceResponseException(response.StatusCode, $"""
                Error on server: {response.StatusCode}
                {await ResponseBodyForce(response)}
                """);

        // Handle 413 HTTP error specifically, as we expect it for files over 25MB
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

    /// <summary>Read the response body and parse as JSON to the expected type.</summary>
    /// <typeparam name="T">Type to parse the response content JSON to</typeparam>
    /// <param name="responseContent">Response from NeverBounce service to parse.</param>
    /// <returns>The result of a successful parse, or throws an exception. Never null.</returns>
    /// <exception cref="NeverBounceResponseException">Thrown if no body content or the content type is not "application/json"</exception>
    /// <exception cref="NeverBounceServiceException">Thrown if the parsed content contains an error status</exception>
    static async Task<T> ParseResponse<T>(HttpContent responseContent) 
        where T : notnull, ResponseModel
    {
        string data = await responseContent.ReadAsStringAsync();
        if(string.IsNullOrWhiteSpace(data))
            throw new NeverBounceResponseException(HttpStatusCode.OK, "No message content");

        // Expects "application/json", but may be "application/json; charset=utf-8"
        string? contentType = responseContent.Headers.ContentType?.ToString();
        if (contentType is null || !contentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
            // Handle unexpected response types
            throw new NeverBounceResponseException(HttpStatusCode.OK, $"""
                The response from NeverBounce was has a data type of "{contentType}", but "application/json" was expected.
                {data}
                """);

        // Handle application/json responses
        var parsed = JsonUtility.Deserialise<T>(data);
        return CheckServiceStatus(parsed);
    }

    /// <summary>Handle non 'success' statuses that return with HTTP success codes but an error message in the body</summary>
    /// <param name="response">Response body</param>
    /// <exception cref="NeverBounceServiceException">Thrown if the parsed content contains an error status</exception>
    static T CheckServiceStatus<T>(T response)
        where T : notnull, ResponseModel
    {
        // Handle non 'success' statuses that return with HTTP success codes but an error message in the body
        var status = response.Status;
        if (status == ResponseStatus.Success)
            return response;

        throw new NeverBounceServiceException(status, $"""
            {(status == ResponseStatus.AuthFailure ?
                "We were unable to authenticate your request" :
                "We were unable to complete your request ")} ({SnakeCase.Convert(status.ToString())})
            {response.Message}
            """);
    }

    /// <summary>Convert a size in bytes to more readable KB, MB or GB</summary>
    static string ByteSize(int bytes) {
        if (bytes < 1024) return $"{bytes} Bytes";
        else if (bytes < 1048576) return $"{Math.Round((double)bytes / 1024d, 2)} KB";
        else if (bytes < 1073741824) return $"{Math.Round((double)bytes / 1048576d, 2)} MB";
        else return $"{Math.Round((double)bytes / 1073741824d, 2)} GB";;
    }
}
