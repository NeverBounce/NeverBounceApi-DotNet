using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Services
{
    class JobService
    {
        public static async Task<JobSearchResponseModel> SearchJob(string serverAddress, JobSearchRequestModel model)
        {
            NeverBounceHttpClient client = new NeverBounceHttpClient(serverAddress);
            var result = await client.MakeRequest("GET", "/jobs/search", model);
            return JsonConvert.DeserializeObject<JobSearchResponseModel>(result.json.ToString());
        }

        public static async Task<JobCreateResponseModel> CreateJob(string serverAddress, JobCreateRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(serverAddress);
            var result = await client.MakeRequest("POST", "/jobs/create",  model);
            return JsonConvert.DeserializeObject<JobCreateResponseModel>(result.json.ToString());
        }

        public static async Task<JobParseResponseModel> ParseJob(string serverAddress, JobParseRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(serverAddress);
			var result = await client.MakeRequest("POST", "/jobs/parse", model);
			return JsonConvert.DeserializeObject<JobParseResponseModel>(result.json.ToString());
        }

        public static async Task<JobStartResponseModel> StartJob(string serverAddress, JobStartRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(serverAddress);
			var result = await client.MakeRequest("POST", "/jobs/start", model);
			return JsonConvert.DeserializeObject<JobStartResponseModel>(result.json.ToString());
        }

        public static async Task<JobStatusResponseModel> JobStatus(string serverAddress, JobStatusRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(serverAddress);
			var result = await client.MakeRequest("GET", "/jobs/status", model);
            return JsonConvert.DeserializeObject<JobStatusResponseModel>(result.json.ToString());
        }

        public static async Task<JobResultsResponseModel> JobResults(string serverAddress, JobResultsRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(serverAddress);
			var result = await client.MakeRequest("GET", "/jobs/results", model);
            return JsonConvert.DeserializeObject<JobResultsResponseModel>(result.json.ToString());
        }
		public static async Task<JobDeleteResponseModel> DeleteJobs(string serverAddress, JobDeleteRequestModel model)
		{
			NeverBounceHttpClient client = new NeverBounceHttpClient(serverAddress);
			var result = await client.MakeRequest("GET", "/jobs/delete", model);
			return JsonConvert.DeserializeObject<JobDeleteResponseModel>(result.json.ToString());
		}

		public static async Task<String> DownloadJobData(string serverAddress, JobDownloadRequestModel model)
		{
			NeverBounceHttpClient client = new NeverBounceHttpClient(serverAddress);
			var result = await client.MakeRequest("GET", "/jobs/download", model);
			return result.plaintext;
		}
    }
}
