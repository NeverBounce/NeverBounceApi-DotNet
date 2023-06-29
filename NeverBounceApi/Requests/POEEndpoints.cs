using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests;

public class POEEndpoints
{
    public static async Task<POEConfirmResponseModel> Confirm(NeverBounceService sdk)
    {
        var model = new POEConfirmRequestModel();
        model.Email = "support@neverbounce.com";
        model.ConfirmationToken = "e3173fdbbdce6bad26522dae792911f2";
        model.TransactionID = "NBPOE-TXN-5942940c09669";
        model.Result = "valid";
        return await sdk.POE.Confirm(model);
    }
}