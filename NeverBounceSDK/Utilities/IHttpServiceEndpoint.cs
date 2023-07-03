namespace NeverBounce.Utilities;
using System.Net.Http.Headers;

public interface IHttpServiceEndpoint
{
    Task<HttpResponseMessage> GetAsync(string? uri, CancellationToken cancellationToken);

    Task<HttpResponseMessage> PostAsync(string? uri, HttpContent content, CancellationToken cancellationToken);
}
