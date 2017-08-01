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
        private static string jobSearch = "/jobs/search";
        private static string jobcreate = "/jobs/create";
        private static string jobParse = "/jobs/parse";
        private static string jobStart = "/jobs/start";
        private static string jobStatus = "/jobs/status";
        private static string jobResult = "/jobs/results";
        private static string jobDownload = "/jobs/download";
        private static string jobDelete = "/jobs/delete";
      
        public static async Task<JobSearchResponseModel> SearchJob(string serverAddress, JobSearchRequestModel model)
        {
            try
            {
                SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
                var json = ConvertDictionary(model);
                var result = await uitylity.GetNeverBounce(serverAddress + jobSearch, GenerateQuerstring(json));
                JobSearchResponseModel startJob = JsonConvert.DeserializeObject<JobSearchResponseModel>(result.ToString());
                Log.WriteLog(": " + result.ToString());
                return startJob;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static async Task<JobCreateResponseModel> CreateJob(string serverAddress, JobCreateRequestModel model)
        {
            try
            {
                JobCreateResponseModel createJob = null;
                if (Validation.JobCreateValidation(model))
                {
                    SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
                    var json = JsonConvert.SerializeObject(model);
                    var result = await uitylity.PostNeverBounce(serverAddress + jobcreate, json);
                    createJob = JsonConvert.DeserializeObject<JobCreateResponseModel>(result.ToString());
                }
                else
                {
                    createJob = new JobCreateResponseModel();
                    createJob.message = "Missing required parameters";
                    createJob.status = "general_failure";
                }
                return createJob;
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public static async Task<JobParseResponseModel> ParseJob(string serverAddress, JobParseRequestModel model)
        {
            try
            {
                JobParseResponseModel parseJob = null;
                if (Validation.JobParseValidation(model))
                {
                    SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
                    var json = JsonConvert.SerializeObject(model);
                    var result = await uitylity.PostNeverBounce(serverAddress + jobParse, json);
                    parseJob = JsonConvert.DeserializeObject<JobParseResponseModel>(result.ToString());
                }
                else
                {
                    parseJob = new JobParseResponseModel();
                    parseJob.message = "Missing required parameters";
                    parseJob.status = "general_failure";
                }
                return parseJob;
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public static async Task<JobStartResponseModel> StartJob(string serverAddress, JobStartRequestModel model)
        {
            try
            {
                JobStartResponseModel startJob = null;
                if (Validation.JobIdValidation(model.job_id))
                {
                    SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
                    var json = JsonConvert.SerializeObject(model);
                    var result = await uitylity.PostNeverBounce(serverAddress + jobStart, json);
                    startJob = JsonConvert.DeserializeObject<JobStartResponseModel>(result.ToString());
                }
                else
                {
                    startJob = new JobStartResponseModel();
                    startJob.message = "Missing required parameter 'job_id'";
                    startJob.status = "general_failure";
                }
                return startJob;
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public static async Task<JobStatusResponseModel> JobStatus(string serverAddress, JobStatusRequestModel model)
        {
            try
            {
                JobStatusResponseModel statusJob = null;
                if (Validation.JobIdValidation(model.job_id))
                {
                    SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
                    var json = JsonConvert.SerializeObject(model);
                    //string queryString = "?";
                    //queryString += json.Replace(":", "=").Replace("{", "").
                    //            Replace("}", "").Replace(",", "&").
                    //                Replace("\"", "");
                    var result = await uitylity.GetNeverBounce(serverAddress + jobStatus, GenerateQuerstring(json));
                    statusJob = JsonConvert.DeserializeObject<JobStatusResponseModel>(result.ToString());
                }
                else
                {
                    statusJob = new JobStatusResponseModel();
                    statusJob.message = "Missing required parameter 'job_id'";
                    statusJob.status = "general_failure";
                }
                return statusJob;
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public static async Task<JobResultsResponseModel> JobResults(string serverAddress, JobResultsRequestModel model)
        {
            try
            {
                JobResultsResponseModel jobResults = null;
                if (Validation.JobIdValidation(model.job_id))
                {
                    SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
                    var json = ConvertResultDictionary(model);
                    var result = await uitylity.GetNeverBounce(serverAddress + jobResult, GenerateQuerstring(json));
                    jobResults = JsonConvert.DeserializeObject<JobResultsResponseModel>(result.ToString());
                }
                else
                {

                    jobResults = new JobResultsResponseModel();
                    jobResults.message = "Missing required parameter 'job_id'";
                    jobResults.status = "general_failure";
                }
                return jobResults;
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public static async Task<JobDeleteResponseModel> DeleteJobs(string serverAddress, JobDeleteRequestModel model)
        {
            try
            {
                JobDeleteResponseModel deleteJob = null;
                if (Validation.JobIdValidation(model.job_id))
                {
                    SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
                    var json = JsonConvert.SerializeObject(model);
                    var result = await uitylity.PostNeverBounce(serverAddress + jobDelete, json);
                    deleteJob = JsonConvert.DeserializeObject<JobDeleteResponseModel>(result.ToString());
                }
                else
                {
                    deleteJob = new JobDeleteResponseModel();
                    deleteJob.message = "Missing required parameter 'job_id'";
                    deleteJob.status = "general_failure";
                }
                return deleteJob;
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        private static string ConvertDictionary(JobSearchRequestModel model)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (model.key != null)
            {
                data["key"] = model.key;
            }
            if (model.job_id != 0)
            {
                data["job_id"] = model.job_id;
            }
            if (!string.IsNullOrEmpty(model.filename)) 
            {
                data["filename"] = model.filename;
            }
            if (model.job_status != null&& model.job_status!="")
            {
                data["job_status"] = model.job_status;
            }
            if (model.page != 0)
            {
                data["page"] = model.page;
            }
            if (model.items_per_page != 0)
            {
                data["items_per_page"] = model.items_per_page;
            }
            //var json = JsonConvert.SerializeObject(model);
            return JsonConvert.SerializeObject(data);
        }
        private static string ConvertResultDictionary(JobResultsRequestModel model)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (model.key != null)
            {
                data["key"] = model.key;
            }
            if (model.job_id > 0)
            {
                data["job_id"] = model.job_id;
            }
            if (model.items_per_page > 0)
            {
                data["items_per_page"] = model.items_per_page;
            }
            if (model.page > 0)
            {
                data["page"] = model.page;
            }
            return JsonConvert.SerializeObject(data);
        }
        private static string GenerateQuerstring(string parameters)
        {
            //var json = JsonConvert.SerializeObject(model);
            string str = "?";
            str += parameters.Replace(":", "=").Replace("{", "").
                        Replace("}", "").Replace(",", "&").
                            Replace("\"", "");

            return str;
        }

        public static async Task<String> DownloadJobData(string serverAddress, JobDownloadRequestModel model) {
            try
            {
                string downloadJob = null;
                if (Validation.JobIdValidation(model.job_id))
                {
                    SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);

                    var json = JsonConvert.SerializeObject(model);
                    var result = await uitylity.PostNeverBouncedownload(serverAddress + jobDownload, json);
                    
                    downloadJob = result.ToString();
                }
                
                return downloadJob;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
       
    }
}
