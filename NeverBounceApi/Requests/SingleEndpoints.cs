using NeverBounce;
using NeverBounce.Models;

public class SingleEndpoints
{
    public static async Task<SingleResponseModel> Check(NeverBounceService sdk)
    {
        return await sdk.Single.Check("support@neverbounce.com", true, true);
    }
}