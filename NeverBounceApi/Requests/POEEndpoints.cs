using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests;

public class POEEndpoints
{
    public static async Task<POEConfirmResponseModel> Confirm(NeverBounceService sdk)
    {
        var model = new POEConfirmRequestModel();
        model.email = "support@neverbounce.com";
        model.confirmation_token = "e3173fdbbdce6bad26522dae792911f2";
        model.transaction_id = "NBPOE-TXN-5942940c09669";
        model.result = "valid";
        return await sdk.POE.Confirm(model);
    }
}