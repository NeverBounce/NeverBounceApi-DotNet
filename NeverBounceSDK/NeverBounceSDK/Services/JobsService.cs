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
    public class JobsService
    {
		protected string ApiKey;

		protected string Host;

        public JobsService(string ApiKey, string Host = null)
		{
			this.ApiKey = ApiKey;

			// Accept debug host
			if (Host != null)
				this.Host = Host;
		}

        public async Task<JobSearchResponseModel> Search(JobSearchRequestModel model)
        {
            NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
            var result = await client.MakeRequest("GET", "/jobs/search", model);
            return JsonConvert.DeserializeObject<JobSearchResponseModel>(result.json.ToString());
        }

        public async Task<JobCreateResponseModel> Create(JobCreateRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
            var result = await client.MakeRequest("POST", "/jobs/create",  model);
            return JsonConvert.DeserializeObject<JobCreateResponseModel>(result.json.ToString());
        }

        public async Task<JobParseResponseModel> Parse(JobParseRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
			var result = await client.MakeRequest("POST", "/jobs/parse", model);
			return JsonConvert.DeserializeObject<JobParseResponseModel>(result.json.ToString());
        }

        public async Task<JobStartResponseModel> Start(JobStartRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
			var result = await client.MakeRequest("POST", "/jobs/start", model);
			return JsonConvert.DeserializeObject<JobStartResponseModel>(result.json.ToString());
        }

        public async Task<JobStatusResponseModel> Status(JobStatusRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
			var result = await client.MakeRequest("GET", "/jobs/status", model);
            return JsonConvert.DeserializeObject<JobStatusResponseModel>(result.json.ToString());
        }

        public async Task<JobResultsResponseModel> Results(JobResultsRequestModel model)
        {
			NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
			var result = await client.MakeRequest("GET", "/jobs/results", model);
            return JsonConvert.DeserializeObject<JobResultsResponseModel>(result.json.ToString());
        }

		public async Task<String> Download(JobDownloadRequestModel model)
		{
			NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
			var result = await client.MakeRequest("GET", "/jobs/download", model);
			return result.plaintext;
		}

		public async Task<JobDeleteResponseModel> Delete(JobDeleteRequestModel model)
		{
			NeverBounceHttpClient client = new NeverBounceHttpClient(ApiKey, Host);
			var result = await client.MakeRequest("GET", "/jobs/delete", model);
			return JsonConvert.DeserializeObject<JobDeleteResponseModel>(result.json.ToString());
		}
    }
}
