using System;
using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class POEEndpoints
    {
        public static POEConfirmResponseModel Confirm(NeverBounceSdk sdk)
        {
            POEConfirmRequestModel model = new POEConfirmRequestModel();
			model.email = "support@neverbounce.com";
            model.confirmation_token = "e3173fdbbdce6bad26522dae792911f2";
			model.transaction_id = "NBPOE-TXN-5942940c09669";
			model.result = "valid";
			return sdk.POE.Confirm(model).Result;
		}
    }
}
