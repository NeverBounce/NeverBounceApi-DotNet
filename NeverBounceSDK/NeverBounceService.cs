using NeverBounce.Services;
using NeverBounce.Utilities;

namespace NeverBounce;

public class NeverBounceService
{
    private const string _host = "https://api.neverbounce.com";
    private readonly string _apiKey;
    private readonly IHttpClient _client;
    private readonly string _version;

    public readonly AccountService Account;
    public readonly JobsService Jobs;
    public readonly POEService POE;
    public readonly SingleService Single;

    /// <summary>
    ///     This method initializes the NeverBounceSDK
    /// </summary>
    /// <param name="ApiKey">The api key to use to make the requests</param>
    /// <param name="Version">The api version to make this request on</param>
    /// <param name="Host">Specify a different host to make the request to. Leave null to use 'https://api.neverbounce.com'</param>
    /// <param name="Client">An instance of IHttpClient to use; useful for mocking HTTP requests</param>
    public NeverBounceService(string ApiKey, string Version = "v4.2", string Host = null, IHttpClient Client = null)
    {
        this._apiKey = ApiKey;
        this._version = Version;

        // Check for mocked IHttpClient, if none exists create default
        this._client = Client ?? new HttpClientWrapper();

        var url = $"{Host ?? _host}/{this._version}";

        this.Account = new AccountService(this._client, this._apiKey, url);
        this.Jobs = new JobsService(this._client, this._apiKey, url);
        this.POE = new POEService(this._client, this._apiKey, url);
        this.Single = new SingleService(this._client, this._apiKey, url);
    }
}