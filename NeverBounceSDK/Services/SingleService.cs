namespace NeverBounce.Services;
using NeverBounce.Models;
using NeverBounce.Utilities;

public sealed class SingleService
{
    readonly INeverBounceEndpoint client;

    internal SingleService(INeverBounceEndpoint client)
    {
        this.client = client;
    }

    /// <summary>Single verification allows you verify individual emails and gather additional information pertaining to the email.
    /// <para>Each verification performed over the Single endpoints cost 1 credit. 
    /// This includes duplicate verifications requests and bad syntax data.</para></summary>
    /// <param name="model">The model details to request</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Result of checking the email</returns>
    public async Task<SingleResponseModel> Check(SingleRequestModel model, CancellationToken? cancellationToken = null) =>
        await this.client.RequestGet<SingleResponseModel>("single/check", model, cancellationToken);

    /// <summary>Single verification allows you verify individual emails and gather additional information pertaining to the email.
    /// <para>Each verification performed over the Single endpoints cost 1 credit. 
    /// This includes duplicate verifications requests and bad syntax data.</para></summary>
    /// <param name="email">The email to check</param>
    /// <param name="addressInfo">Include additional address info in response</param>
    /// <param name="creditsInfo">Include account credit info in response</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Result of checking the email</returns>
    public async Task<SingleResponseModel> Check(string email, bool addressInfo = false, bool creditsInfo = false, CancellationToken? cancellationToken = null) =>
        await this.Check(new SingleRequestModel(email) { AddressInfo = addressInfo, CreditsInfo = creditsInfo }, cancellationToken);
}
