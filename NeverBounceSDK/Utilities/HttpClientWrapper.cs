using System.Net.Http.Headers;

namespace NeverBounce.Utilities;

public interface IHttpClient
{
    HttpRequestHeaders GetRequestHeaders();
    Task<HttpResponseMessage> GetAsync(Uri uri);
    Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent content);
}

public class HttpClientWrapper : IHttpClient
{
    private readonly HttpClient _client;
    
    public HttpClientWrapper()
    {
        this._client = new HttpClient();
    }

    public HttpRequestHeaders GetRequestHeaders()
    {
        return this._client.DefaultRequestHeaders;
    }

    public Task<HttpResponseMessage> GetAsync(Uri uri)
    {
        return this._client.GetAsync(uri);
    }

    public Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent content)
    {
        return this._client.PostAsync(uri, content);
    }
}