namespace NeverBounce; 
using NeverBounce.Models;
using NeverBounce.Services;
using NeverBounce.Utilities;

public class NeverBounceService
{
    private const string DEFAULT_HOST = "https://api.neverbounce.com";
    private readonly INeverBounceHttpClient client;

    public readonly AccountService Account;
    public readonly JobsService Jobs;
    public readonly SingleService Single;

    /// <summary>
    ///     This method initializes the NeverBounceSDK
    /// </summary>
    /// <param name="ApiKey">The api key to use to make the requests</param>
    /// <param name="Version">The api version to make this request on</param>
    /// <param name="Host">Specify a different host to make the request to. Leave null to use 'https://api.neverbounce.com'</param>
    /// <param name="Client">An instance of IHttpClient to use; useful for mocking HTTP requests</param>
    public NeverBounceService(IHttpClient httpClient, NeverBounceConfigurationSettings config)
    {
        this.client = new NeverBounceHttpClient(httpClient, new NeverBounceSettings(config.Key, $"{config.Host ?? DEFAULT_HOST}/{config.Version}"));

        this.Account = new AccountService(this.client);
        this.Jobs = new JobsService(this.client);
        this.Single = new SingleService(this.client);
    }
}