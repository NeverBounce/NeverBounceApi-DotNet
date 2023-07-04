namespace NeverBounce.Utilities;
using NeverBounce.Models;
using NeverBounce.Exceptions;

interface INeverBounceEndpoint
{
    /// <summary>This method makes the HTTP request to the API, but makes no attempt to parse it (other than checking HTTP status codes).</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>The content of the response.</returns>
    /// <exception cref="NeverBounceResponseException">Thrown when the NeverBounce service returns an error HTTP status code.</exception>
    Task<HttpContent> RequestGetContent(string endpoint, object? model = null, CancellationToken? cancellationToken = null);

    /// <summary>This method makes the HTTP request to the API and parses the response</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <typeparam name="T">The expected response model to parse</typeparam>
    /// <returns>The parsed result, or null if no content.</returns>
    /// <exception cref="NeverBounceResponseException">Thrown when the NeverBounce service returns an error HTTP status code.</exception>
    /// <exception cref="NeverBounceParseException">Thrown when there is an error parsing the response from the server.</exception>
    /// <exception cref="NeverBounceServiceException">Thrown when the NeverBounce service returns an error message.</exception>
    Task<T> RequestGet<T>(string endpoint, object? model = null, CancellationToken? cancellationToken = null) 
        where T : notnull, ResponseModel;


    /// <summary>This method makes the HTTP request to the API and parses the response</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <typeparam name="T">The expected response model to parse</typeparam>
    /// <returns>The parsed result, or null if no content.</returns>
    /// <exception cref="NeverBounceResponseException">Thrown when the NeverBounce service returns an error HTTP status code.</exception>
    /// <exception cref="NeverBounceParseException">Thrown when there is an error parsing the response from the server.</exception>
    /// <exception cref="NeverBounceServiceException">Thrown when the NeverBounce service returns an error message.</exception>
    Task<T> RequestPost<T>(string endpoint, object model, CancellationToken? cancellationToken = null) 
        where T : notnull, ResponseModel;
}
