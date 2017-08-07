using NeverBounce.Models;
using NeverBounce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace NeverBounce
{
   public class NeverBounceSdk
    {
        private string ApiKey;
        private String ServerAddress = "https://api.neverbounce.com/v4";
        //string abc= NLog.Internal.ConfigurationManager.AppSettings 
        public  NeverBounceSdk(String _ServerAddress, string ApiKey)
        {

            this.ApiKey = ApiKey;
            this.ServerAddress = _ServerAddress;
        }

        /// <summary>
        /// This method call the account info end points "https://api.neverbounce.com/v4/account/info"
        /// </summary>
        /// <returns>AccountInfoModel</returns>
        public async Task<AccountInfoModel> AccountInfo()
        {
            RequestModel model = new RequestModel();
			model.key = ApiKey;
			return await AccountService.AccountInfo(ServerAddress, model);
        }
        /// <summary>
        /// This method call the single/check end points "https://api.neverbounce.com/v4/single/check
        /// </summary>
        /// <param name="SingleRequestModel.email"> email (Required) The email to verify</param>
        /// <param name="SingleRequestModel.address_info"> address_info (optional) Include additional address info in response </param>
        /// <param name="SingleRequestModel.credits_info"> credits_info(optional) Include account credit info in response </param>
        /// <param name="SingleRequestModel.timeout">timeout (optional)The maximum time in seconds we should try to verify the address</param>
        /// <returns>SingleModel</returns>
        public async Task<SingleModel> SingleCheck(SingleRequestModel model)
        {
            model.key = ApiKey;
            return await SingleService.Check(ServerAddress, model);
        }
        /// <summary>
        /// This method call the search job end points "https://api.neverbounce.com/v4/jobs/search"
        /// </summary>
        /// <param name="JobSearchRequestModel.job_id"> job_id (optional) isFilter jobs based on it's id</param>
        /// <param name="JobSearchRequestModel.filename"> filename (optional) is Filter jobs based on the filename (exact match) </param>
        /// <param name="JobSearchRequestModel.job_status"> job_status(optional) is Filter jobs by the job status </param>
        /// <param name="JobSearchRequestModel.page">page (optional) The page to grab the jobs from</param>
        /// <param name="JobSearchRequestModel.items_per_page">items_per_page(optional) The number of jobs to display </param>
        /// <returns>JobSearchResponseModel</returns>
        public async Task<JobSearchResponseModel> SearchJob(JobSearchRequestModel model)
        {
            model.key = ApiKey;
            return await JobService.SearchJob(ServerAddress, model);
        }
        /// <summary>
        ///This method call the create job end point "https://api.neverbounce.com/v4/jobs/create"
        /// </summary>
        /// <param name="JobCreateRequestModel"></param>
        /// <param name="JobCreateRequestModel.input_location"> input_location (Required) The type of input being supplied. Accepted values are "remote_url" and "supplied"</param>
        /// <param name="JobCreateRequestModel.input"> input (Required) array of objects the input to be verified </param>
        /// <param name="JobCreateRequestModel.auto_parse"> auto_parse(Required) Should be begin parsing the job immediately? 0 for No 1 for Yes</param>
        /// <param name="JobCreateRequestModel.auto_start">auto_start (Required) Should we run the job immediately after being parsed? 0 for No 1 for Yes </param>
        /// <param name="JobCreateRequestModel.run_sample">run_sample(optional) Should this job be run as a sample? </param>
        /// <param name="JobCreateRequestModel.filename">filename(optional) This will be what's displayed in the dashboard when viewing this job </param>
        /// <returns>JobCreateResponseModel</returns>
        public async Task<JobCreateResponseModel> CreateJob(JobCreateRequestModel model)
        {
            model.key = ApiKey;
            return await JobService.CreateJob(ServerAddress, model);
        }
        /// <summary>
        /// This method call the parse job end point "https://api.neverbounce.com/v4/jobs/parse"
        /// </summary>
        /// <param name="JobParseRequestModel"></param>
        /// <param name="JobParseRequestModel.job_id"> job_id (Required) The id of the job to parse</param>
        /// <param name="JobParseRequestModel.auto_start"> auto_start (Required) Should the job start processing immediately after it's parsed? </param>
        /// <returns>JobParseResponseModel</returns>
        public async Task<JobParseResponseModel> ParseJob(JobParseRequestModel model)
        {
            model.key = ApiKey;
            return await JobService.ParseJob(ServerAddress, model);
           
        }
        /// <summary>
        /// This method call the start job end point "https://api.neverbounce.com/v4/jobs/start"
        /// </summary>
        /// <param name="JobStartRequestModel"></param>
        /// <param name="JobStartRequestModel">api_key is required</param>
        /// <param name="JobStartRequestModel.job_id"> job_id (Required) The id of the job to start</param>
        /// <param name="JobStartRequestModel.run_sample"> run_sample (Optional) Should this job be run as a sample? </param>
        /// <returns>JobStartResponseModel</returns>
        public async Task<JobStartResponseModel> StartJob(JobStartRequestModel model)
        {
            model.key = ApiKey;
            return await JobService.StartJob(ServerAddress, model);
        }
        /// <summary>
        /// This method call the job status end point "https://api.neverbounce.com/v4/jobs/status"
        /// </summary>
        /// <param name="JobStatusRequestModel.job_id"> Job_id(Required) The id of the job to check</param>
        /// <returns>JobStatusResponseModel</returns>
        public async Task<JobStatusResponseModel> JobStatus(JobStatusRequestModel model)
        {
            model.key = ApiKey;
            return await JobService.JobStatus(ServerAddress, model);
        }
        /// <summary>
        /// This method call the job results end point "https://api.neverbounce.com/v4/jobs/results"
        /// </summary>
        /// <param name="JobResultsRequestModel.job_id">job_id(Required)The id of the job to retrieve results for</param>
        /// <param name="JobResultsRequestModel.page">page(optional) The page to return the results from</param>
        /// <param name="JobResultsRequestModel.items_per_page">items_per_page (optional)The number of results to display</param>
        /// <returns>JobResultsResponseModel</returns>
        public async Task<JobResultsResponseModel> JobResults(JobResultsRequestModel model)
        {
            model.key = ApiKey;
            return await JobService.JobResults(ServerAddress, model);
        }
        /// <summary>
        /// This method call the delete job end point "https://api.neverbounce.com/v4/jobs/delete"
        /// </summary>
        /// <param name="JobDeleteRequestModel.job_id">job_id(Required) The id of the job to download</param>
        /// <returns>JobDeleteResponseModel</returns>
        public async Task<JobDeleteResponseModel> DeleteJobs(JobDeleteRequestModel model)
        {
            model.key = ApiKey;
            return await JobService.DeleteJobs(ServerAddress, model);
        }
        /// <summary>
        /// This method call the download job end point "https://api.neverbounce.com/v4/jobs/download"
        /// </summary>
        /// <param name="JobDownloadRequestModel"></param>
        /// <returns>JobDownloadResponseModel</returns>
        public async Task<String> DownloadJob(JobDownloadRequestModel model)
        {
            model.key = ApiKey;
            return await JobService.DownloadJobData(ServerAddress, model);
        }

    }
}
