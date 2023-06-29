using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests;

public class SingleEndpoints
{
    public static async Task<SingleResponseModel> Check(NeverBounceService sdk)
    {
        var model = new SingleRequestModel();
        model.Email = "support@neverbounce.com";
        model.CreditsInfo = true;
        model.AddressInfo = true;
        return await sdk.Single.Check(model);
    }
}