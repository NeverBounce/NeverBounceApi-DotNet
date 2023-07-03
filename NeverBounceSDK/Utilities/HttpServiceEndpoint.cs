namespace NeverBounce.Utilities;
using System.Net.Http.Headers;
using System.Threading;


class HttpServiceEndpoint : IHttpServiceEndpoint
{
    readonly HttpClient client;
    
    public HttpServiceEndpoint(HttpClient client)
    {
        this.client = client;
    }

    public Task<HttpResponseMessage> GetAsync(string? uri, CancellationToken cancellationToken) =>
        this.client.GetAsync(uri, cancellationToken);
   
    public Task<HttpResponseMessage> PostAsync(string? uri, HttpContent content, CancellationToken cancellationToken) => 
        this.client.PostAsync(uri, content, cancellationToken);
}
