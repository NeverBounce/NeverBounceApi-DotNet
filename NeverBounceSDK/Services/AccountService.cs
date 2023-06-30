namespace NeverBounce.Services;
using NeverBounce.Models;
using NeverBounce.Utilities;

public sealed class AccountService
{
    readonly INeverBounceHttpClient client;

    internal AccountService(INeverBounceHttpClient client)
    {
        this.client = client;
    }

    /// <summary>Check your account's balance and how many jobs are currently running</summary>
    /// <returns>AccountInfoResponseModel</returns>
    public async Task<AccountInfoResponseModel?> Info() => 
        await this.client.RequestGet<AccountInfoResponseModel>("/account/info");
}
