namespace NeverBounce.Utilities;
using System.Net.Http.Headers;

public class HttpClientWrapper : IHttpClient
{
    readonly HttpClient _client;
    
    public HttpClientWrapper(HttpClient client)
    {
        this._client = client;
    }

    public HttpRequestHeaders DefaultRequestHeaders =>
        this._client.DefaultRequestHeaders;

    public Task<HttpResponseMessage> GetAsync(Uri uri) =>
        this._client.GetAsync(uri);
   
    public Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent content) => 
        this._client.PostAsync(uri, content);
}
