using NeverBounce;
using NeverBounce.Models;

public class SingleEndpoints
{
    public static async Task<SingleResponseModel> Check(NeverBounceService sdk)
    {
        return await sdk.CheckSingle("support@neverbounce.com", true, true);
    }
}