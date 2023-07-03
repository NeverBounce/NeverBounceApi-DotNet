namespace NeverBounce.Services;
using NeverBounce.Models;
using NeverBounce.Utilities;

public sealed class AccountService
{
    readonly INeverBounceEndpoint client;

    internal AccountService(INeverBounceEndpoint client)
    {
        this.client = client;
    }

    /// <summary>Check your account's balance and how many jobs are currently running</summary>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Current credits and running jobs.</returns>
    public async Task<AccountInfoResponseModel> Info(CancellationToken? cancellationToken = null) =>
        await this.client.RequestGet<AccountInfoResponseModel>("account/info", cancellationToken);
}
