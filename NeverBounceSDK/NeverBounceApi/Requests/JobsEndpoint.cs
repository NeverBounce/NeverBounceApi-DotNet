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
            return sdk.SearchJob(model).Result;
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
            return sdk.CreateJob(model).Result;
		}

		public static ResponseModel Parse(NeverBounceSdk sdk)
		{
            JobParseRequestModel model = new JobParseRequestModel();
            model.job_id = 290078;
            return sdk.ParseJob(model).Result;
		}

		public static ResponseModel Start(NeverBounceSdk sdk)
		{
            JobStartRequestModel model = new JobStartRequestModel();
			model.job_id = 290078;
            return sdk.StartJob(model).Result;
		}

		public static ResponseModel Status(NeverBounceSdk sdk)
		{
            JobStatusRequestModel model = new JobStatusRequestModel();
			model.job_id = 290078;
            return sdk.JobStatus(model).Result;
		}

		public static ResponseModel Results(NeverBounceSdk sdk)
		{
            JobResultsRequestModel model = new JobResultsRequestModel();
			model.job_id = 290078;
            return sdk.JobResults(model).Result;
		}

        public static String Download(NeverBounceSdk sdk)
		{
            JobDownloadRequestModel model = new JobDownloadRequestModel();
			model.job_id = 290078;
            return sdk.DownloadJob(model).Result;
		}

		public static ResponseModel Delete(NeverBounceSdk sdk)
		{
            JobDeleteRequestModel model = new JobDeleteRequestModel();
			model.job_id = 290078;
            return sdk.DeleteJobs(model).Result;
		}
    }
}
