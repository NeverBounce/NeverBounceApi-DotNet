using NeverBounce.Models;
using NeverBounce.Utilities;
using Newtonsoft.Json;

namespace NeverBounce.Services
{
    public class JobsService
    {
        protected string _apiKey;

        protected IHttpClient _client;

        protected string _host;

        public JobsService(IHttpClient Client, string ApiKey, string Host = null)
        {
            this._client = Client;
            this._apiKey = ApiKey;

            // Accept debug host
            if (Host != null)
                this._host = Host;
        }

        /// <summary>
        ///     This method calls the search job end points.
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-search"
        /// </summary>
        /// <param name="model">JobSearchRequestModel</param>
        /// <returns>JobSearchResponseModel</returns>
        public async Task<JobSearchResponseModel> Search(JobSearchRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("GET", "/jobs/search", model);
            return JsonConvert.DeserializeObject<JobSearchResponseModel>(result);
        }

        /// <summary>
        ///     This method calls the create job end point using supplied data for input
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-create"
        /// </summary>
        /// <param name="model">JobCreateRequestModel</param>
        /// <returns>JobCreateResponseModel</returns>
        public async Task<JobCreateResponseModel> CreateFromSuppliedData(JobCreateSuppliedDataRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("POST", "/jobs/create", model);
            return JsonConvert.DeserializeObject<JobCreateResponseModel>(result);
        }

        /// <summary>
        ///     This method calls the create job end point using a remote URL for the input
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-create"
        /// </summary>
        /// <param name="model">JobCreateRemoteUrlRequestModel</param>
        /// <returns>JobCreateResponseModel</returns>
        public async Task<JobCreateResponseModel> CreateFromRemoteUrl(JobCreateRemoteUrlRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("POST", "/jobs/create", model);
            return JsonConvert.DeserializeObject<JobCreateResponseModel>(result);
        }

        /// <summary>
        ///     This method calls the parse job end point
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-parse"
        /// </summary>
        /// <param name="model">JobParseRequestModel</param>
        /// <returns>JobParseResponseModel</returns>
        public async Task<JobParseResponseModel> Parse(JobParseRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("POST", "/jobs/parse", model);
            return JsonConvert.DeserializeObject<JobParseResponseModel>(result);
        }

        /// <summary>
        ///     This method calls the start job end point
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-start"
        /// </summary>
        /// <param name="model">JobStartRequestModel</param>
        /// <returns>JobStartResponseModel</returns>
        public async Task<JobStartResponseModel> Start(JobStartRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("POST", "/jobs/start", model);
            return JsonConvert.DeserializeObject<JobStartResponseModel>(result);
        }

        /// <summary>
        ///     This method calls the job status endpoint
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-status"
        /// </summary>
        /// <param name="model">JobStatusRequestModel</param>
        /// <returns>JobStatusResponseModel</returns>
        public async Task<JobStatusResponseModel> Status(JobStatusRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("GET", "/jobs/status", model);
            return JsonConvert.DeserializeObject<JobStatusResponseModel>(result);
        }

        /// <summary>
        ///     This method calls the job results endpoint
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-results"
        /// </summary>
        /// <param name="model">JobResultsRequestModel</param>
        /// <returns>JobResultsResponseModel</returns>
        public async Task<JobResultsResponseModel> Results(JobResultsRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("GET", "/jobs/results", model);
            return JsonConvert.DeserializeObject<JobResultsResponseModel>(result);
        }

        /// <summary>
        ///     This method calls the job download endpoint; this endpoint returns the
        ///     CSV data for the job
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-download"
        /// </summary>
        /// <param name="model">JobDownloadRequestModel</param>
        /// <returns>string</returns>
        public async Task<string> Download(JobDownloadRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            client.SetAcceptedType("application/octet-stream");
            return await client.MakeRequest("GET", "/jobs/download", model);
        }

        /// <summary>
        ///     This method calls the job delete endpoint
        ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-delete"
        /// </summary>
        /// <param name="model">JobDeleteRequestModel</param>
        /// <returns>JobResultsResponseModel</returns>
        public async Task<JobDeleteResponseModel> Delete(JobDeleteRequestModel model)
        {
            var client = new NeverBounceHttpClient(this._client, this._apiKey, this._host);
            var result = await client.MakeRequest("GET", "/jobs/delete", model);
            return JsonConvert.DeserializeObject<JobDeleteResponseModel>(result);
        }
    }
}
