namespace NeverBounce.Utilities;
using System.Net.Http.Headers;

public interface IHttpClient
{
    HttpRequestHeaders DefaultRequestHeaders { get; }
    Task<HttpResponseMessage> GetAsync(Uri uri);
    Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent content);
}
