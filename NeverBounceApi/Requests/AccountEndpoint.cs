using NeverBounce;
using NeverBounce.Models;

public class AccountEndpoint
{
    public static async Task<AccountInfoResponseModel> Info(NeverBounceService sdk)
    {
        return await sdk.Account.Info();
    }
}