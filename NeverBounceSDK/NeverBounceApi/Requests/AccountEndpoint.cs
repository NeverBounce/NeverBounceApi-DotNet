using System;
using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class AccountEndpoint
    {
        public static ResponseModel Info(NeverBounceSdk sdk)
        {
            return sdk.AccountInfo().Result;
		}
    }
}
