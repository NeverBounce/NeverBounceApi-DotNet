using System;
using System.Collections.Generic;
using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class JobsEndpoint
    {
        public static JobSearchResponseModel Search(NeverBounceSdk sdk)
        {
            JobSearchRequestModel model = new JobSearchRequestModel();
			model.job_id = 288025;
            return sdk.Jobs.Search(model).Result;
		}

        public static JobCreateResponseModel CreateSuppliedData(NeverBounceSdk sdk)
        {
			JobCreateSuppliedDataRequestModel model = new JobCreateSuppliedDataRequestModel();
			model.filename = "Created From dotNET.csv";
            model.auto_parse = true;
            model.auto_start = false;
			List<object> data = new List<object>();
			data.Add(new { id = "3", email = "support@neverbounce.com", name = "Fred McValid" });
			data.Add(new { id = "4", email = "invalid@neverbounce.com", name = "Bob McInvalid" });
			model.input = data;
            return sdk.Jobs.CreateFromSuppliedData(model).Result;
		}

		public static JobCreateResponseModel CreateRemoteUrl(NeverBounceSdk sdk)
		{
			JobCreateRemoteUrlRequestModel model = new JobCreateRemoteUrlRequestModel();
			model.filename = "Created From dotNET.csv";
			model.auto_parse = true;
			model.auto_start = false;
			model.input = "https://example.com/file.csv";
			return sdk.Jobs.CreateFromRemoteUrl(model).Result;
		}

        public static JobParseResponseModel Parse(NeverBounceSdk sdk)
		{
            JobParseRequestModel model = new JobParseRequestModel();
            model.job_id = 290561;
            return sdk.Jobs.Parse(model).Result;
		}

        public static JobStartResponseModel Start(NeverBounceSdk sdk)
		{
            JobStartRequestModel model = new JobStartRequestModel();
			model.job_id = 290561;
            return sdk.Jobs.Start(model).Result;
		}

        public static JobStatusResponseModel Status(NeverBounceSdk sdk)
		{
            JobStatusRequestModel model = new JobStatusRequestModel();
			model.job_id = 290561;
            return sdk.Jobs.Status(model).Result;
		}

        public static JobResultsResponseModel Results(NeverBounceSdk sdk)
		{
            JobResultsRequestModel model = new JobResultsRequestModel();
			model.job_id = 290561;
            return sdk.Jobs.Results(model).Result;
		}

        public static String Download(NeverBounceSdk sdk)
		{
            JobDownloadRequestModel model = new JobDownloadRequestModel();
			model.job_id = 290561;
            return sdk.Jobs.Download(model).Result;
		}

        public static JobDeleteResponseModel Delete(NeverBounceSdk sdk)
		{
            JobDeleteRequestModel model = new JobDeleteRequestModel();
			model.job_id = 290561;
            return sdk.Jobs.Delete(model).Result;
		}
    }
}
