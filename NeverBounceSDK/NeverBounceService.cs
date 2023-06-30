namespace NeverBounce; 
using NeverBounce.Models;
using NeverBounce.Services;
using NeverBounce.Utilities;

public class NeverBounceService
{
    const string DEFAULT_HOST = "https://api.neverbounce.com";
    readonly INeverBounceHttpClient client;

    /// <summary>Account Info method allow to programmatically check your account's balance and how many jobs are currently running on your account.</summary>
    public readonly AccountService Account;

    /// <summary>The bulk endpoint provides high-speed validation on a list of email addresses. 
    /// You can use the status endpoint to retrieve real-time statistics about a bulk job in progress. 
    /// Once the job has finished, the results can be retrieved with our download endpoint. </summary>
    public readonly JobsService Jobs;

    /// <summary>Single verification allows you verify individual emails and gather additional information pertaining to the email.</summary>
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