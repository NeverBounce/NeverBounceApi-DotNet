namespace NeverBounce.Services;
using NeverBounce.Models;
using NeverBounce.Utilities;

public sealed class AccountService
{
    readonly INeverBounceHttpClient client;

    public AccountService(INeverBounceHttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    ///     Account Info method allow to programmatically check your account's balance and how many jobs are currently running
    ///     on your account.
    ///     See: "https://developers.neverbounce.com/v4.0/reference#account-info"
    /// </summary>
    /// <returns>AccountInfoResponseModel</returns>
    public async Task<AccountInfoResponseModel?> Info() => 
        await this.client.RequestGet<AccountInfoResponseModel>("/account/info");
}
