namespace NeverBounce;
using Microsoft.Extensions.Logging;
using NeverBounce.Models;
using NeverBounce.Services;
using NeverBounce.Utilities;

public class NeverBounceService
{
    readonly INeverBounceEndpoint client;

    /// <summary>The bulk endpoint provides high-speed validation on a list of email addresses. 
    /// You can use the status endpoint to retrieve real-time statistics about a bulk job in progress. 
    /// Once the job has finished, the results can be retrieved with our download endpoint. </summary>
    public readonly JobsService Jobs;

    /// <summary>This method initializes the NeverBounceService directly
    /// <para>It should not be called directly, use the dependency injection service instead</para></summary>
    /// <param name="key">The api key to use to make the requests</param>
    /// <param name="httpEndpoint">Configured HTTP endpoint</param>
    /// <param name="loggerFactory">Optional logger</param>
    public NeverBounceService(IHttpServiceEndpoint httpEndpoint, string key, ILoggerFactory? loggerFactory)
    {
        this.client = new NeverBounceEndpoint(httpEndpoint, key, loggerFactory);
        this.Jobs = new JobsService(this.client);
    }

    #region single/check

    /// <summary>Single verification allows you verify individual emails and gather additional information pertaining to the email.
    /// <para>Each verification performed over the Single endpoints cost 1 credit. 
    /// This includes duplicate verifications requests and bad syntax data.</para></summary>
    /// <param name="model">The model details to request</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Result of checking the email</returns>
    public async Task<SingleResponseModel> CheckSingle(SingleRequestModel model, CancellationToken? cancellationToken = null) =>
        await this.client.RequestGet<SingleResponseModel>("single/check", model, cancellationToken);

    /// <summary>Single verification allows you verify individual emails and gather additional information pertaining to the email.
    /// <para>Each verification performed over the Single endpoints cost 1 credit. 
    /// This includes duplicate verifications requests and bad syntax data.</para></summary>
    /// <param name="email">The email to check</param>
    /// <param name="addressInfo">Include additional address info in response</param>
    /// <param name="creditsInfo">Include account credit info in response</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Result of checking the email</returns>
    public async Task<SingleResponseModel> CheckSingle(string email, bool addressInfo = false, bool creditsInfo = false, CancellationToken? cancellationToken = null) =>
        await this.CheckSingle(new SingleRequestModel(email) { AddressInfo = addressInfo, CreditsInfo = creditsInfo }, cancellationToken);

    #endregion

    #region account/info

    /// <summary>Account info method allow to programmatically check your account's balance and how many jobs are currently running on your account.</summary>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Current credits and running jobs.</returns>
    public async Task<AccountInfoResponseModel> Account(CancellationToken? cancellationToken = null) =>
        await this.client.RequestGet<AccountInfoResponseModel>("account/info", cancellationToken);

    #endregion
}