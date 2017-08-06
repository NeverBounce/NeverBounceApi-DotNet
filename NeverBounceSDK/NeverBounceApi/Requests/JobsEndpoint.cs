using System;
using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class Jobs
    {
        public static ResponseModel Search(NeverBounceSdk sdk)
        {
            JobSearchRequestModel model = new JobSearchRequestModel();
            return sdk.SearchJob(model).Result;
		}
    }
}
