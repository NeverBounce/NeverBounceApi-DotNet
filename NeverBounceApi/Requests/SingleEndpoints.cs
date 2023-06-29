using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests;

public class SingleEndpoints
{
    public static async Task<SingleResponseModel> Check(NeverBounceService sdk)
    {
        var model = new SingleRequestModel();
        model.email = "support@neverbounce.com";
        model.credits_info = true;
        model.address_info = true;
        return await sdk.Single.Check(model);
    }
}