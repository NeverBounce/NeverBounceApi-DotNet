namespace NeverBounce.Utilities;
using System.Threading;

sealed class HttpServiceEndpoint : IHttpServiceEndpoint
{
    readonly HttpClient client;
    
    public HttpServiceEndpoint(HttpClient client) { this.client = client; }

    Task<HttpResponseMessage> IHttpServiceEndpoint.GetAsync(string? uri, CancellationToken cancellationToken) =>
        this.client.GetAsync(uri, cancellationToken);
   
    Task<HttpResponseMessage> IHttpServiceEndpoint.PostAsync(string? uri, HttpContent content, CancellationToken cancellationToken) => 
        this.client.PostAsync(uri, content, cancellationToken);
}
