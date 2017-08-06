using System;
using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class Single
    {
        public static ResponseModel Check(NeverBounceSdk sdk)
        {
			SingleRequestModel model = new SingleRequestModel();
			model.email = "support@neverbounce.com";
			model.credits_info = true;
            model.address_info = true;
            return sdk.SingleCheck(model).Result;
		}
    }
}
