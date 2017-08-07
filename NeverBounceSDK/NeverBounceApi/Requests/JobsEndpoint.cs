using System;
using System.Collections.Generic;
using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class JobsEndpoint
    {
        public static ResponseModel Search(NeverBounceSdk sdk)
        {
            JobSearchRequestModel model = new JobSearchRequestModel();
			model.job_id = 288025;
            return sdk.Jobs.Search(model).Result;
		}

        public static ResponseModel Create(NeverBounceSdk sdk)
        {
			JobCreateRequestModel model = new JobCreateRequestModel();
			model.input_location = "supplied";
			model.filename = "Created From dotNET.csv";
            model.auto_parse = true;
            model.auto_start = false;
			List<object> data = new List<object>();
			data.Add(new { id = "3", email = "support@neverbounce.com", name = "Fred McValid" });
			data.Add(new { id = "4", email = "invalid@neverbounce.com", name = "Bob McInvalid" });
			model.input = data;
            return sdk.Jobs.Create(model).Result;
		}

		public static ResponseModel Parse(NeverBounceSdk sdk)
		{
            JobParseRequestModel model = new JobParseRequestModel();
            model.job_id = 290497;
            return sdk.Jobs.Parse(model).Result;
		}

		public static ResponseModel Start(NeverBounceSdk sdk)
		{
            JobStartRequestModel model = new JobStartRequestModel();
			model.job_id = 290497;
            return sdk.Jobs.Start(model).Result;
		}

		public static ResponseModel Status(NeverBounceSdk sdk)
		{
            JobStatusRequestModel model = new JobStatusRequestModel();
			model.job_id = 290497;
            return sdk.Jobs.Status(model).Result;
		}

		public static ResponseModel Results(NeverBounceSdk sdk)
		{
            JobResultsRequestModel model = new JobResultsRequestModel();
			model.job_id = 290497;
            return sdk.Jobs.Results(model).Result;
		}

        public static String Download(NeverBounceSdk sdk)
		{
            JobDownloadRequestModel model = new JobDownloadRequestModel();
			model.job_id = 290497;
            return sdk.Jobs.Download(model).Result;
		}

		public static ResponseModel Delete(NeverBounceSdk sdk)
		{
            JobDeleteRequestModel model = new JobDeleteRequestModel();
			model.job_id = 290497;
            return sdk.Jobs.Delete(model).Result;
		}
    }
}
