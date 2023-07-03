namespace NeverBounce;

using Microsoft.Extensions.Logging;
using NeverBounce.Models;
using NeverBounce.Services;
using NeverBounce.Utilities;

public class NeverBounceService
{

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
    /// <param name="key">The api key to use to make the requests</param>
    /// <param name="httpEndpoint">Configured HTTP endpoint</param>
    /// <param name="loggerFactory">Optional logger</param>
    public NeverBounceService(IHttpServiceEndpoint httpEndpoint, string key, ILoggerFactory? loggerFactory)
    {
        this.client = new NeverBounceHttpClient(httpEndpoint, key, loggerFactory);

        this.Account = new AccountService(this.client);
        this.Jobs = new JobsService(this.client);
        this.Single = new SingleService(this.client);
    }
}