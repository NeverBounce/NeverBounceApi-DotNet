namespace NeverBounce.Utilities;

/// <summary>Simplified wrapper for the HttpClient to make mocking in tests easier</summary>
public interface IHttpServiceEndpoint // Must be public for Moq to work
{
    Task<HttpResponseMessage> GetAsync(string? uri, CancellationToken cancellationToken);

    Task<HttpResponseMessage> PostAsync(string? uri, HttpContent content, CancellationToken cancellationToken);
}
