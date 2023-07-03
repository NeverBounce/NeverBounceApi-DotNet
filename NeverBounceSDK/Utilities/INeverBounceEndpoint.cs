namespace NeverBounce.Utilities;
using NeverBounce.Models;
using System.Threading.Tasks;

public interface INeverBounceEndpoint
{

    /// <summary>This method makes the HTTP request to the API and parses the response</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    /// <typeparam name="T">The expected response model to parse</typeparam>
    /// <returns>The parsed result, or null if no content.</returns>
    Task<T> RequestGet<T>(string endpoint, object? model = null) where T : notnull, ResponseModel;

    /// <summary>This method makes the HTTP request to the API</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    Task<HttpContent> RequestGetContent(string endpoint, object? model = null);

    /// <summary>This method makes the HTTP request to the API and parses the response</summary>
    /// <param name="endpoint">The endpoint to request</param>
    /// <param name="model">The parameters to include with the request</param>
    /// <typeparam name="T">The expected response model to parse</typeparam>
    /// <returns>The parsed result, or null if no content.</returns>
    Task<T> RequestPost<T>(string endpoint, object model) where T : notnull, ResponseModel;
}
