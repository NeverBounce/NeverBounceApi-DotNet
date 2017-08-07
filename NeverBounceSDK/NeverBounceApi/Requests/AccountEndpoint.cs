using System;
using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class AccountEndpoint
    {
        public static AccountInfoResponseModel Info(NeverBounceSdk sdk)
        {
            return sdk.Account.Info().Result;
		}
    }
}
