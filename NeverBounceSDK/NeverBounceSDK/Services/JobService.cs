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
            JobSearchResponseModel startJob = null;
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            var json = ConvertDictionary(model);
            var result = await uitylity.GetNeverBounce(serverAddress + jobSearch, GenerateQuerstring(json));
            if (result.Status == "success")
            {
                startJob = JsonConvert.DeserializeObject<JobSearchResponseModel>(result.Data.ToString());
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }
            return startJob;

        }
        public static async Task<JobCreateResponseModel> CreateJob(string serverAddress, JobCreateRequestModel model)
        {
            JobCreateResponseModel createJob = null;
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            var json = JsonConvert.SerializeObject(model);
            var result = await uitylity.PostNeverBounce(serverAddress + jobcreate, json);
            if (result.Status == "success")
            {
                createJob = JsonConvert.DeserializeObject<JobCreateResponseModel>(result.Data.ToString());
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }
            return createJob;
        }
        public static async Task<JobParseResponseModel> ParseJob(string serverAddress, JobParseRequestModel model)
        {
            JobParseResponseModel parseJob = null;
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            var json = JsonConvert.SerializeObject(model);
            var result = await uitylity.PostNeverBounce(serverAddress + jobParse, json);
            if (result.Status == "success")
            {
                parseJob = JsonConvert.DeserializeObject<JobParseResponseModel>(result.Data.ToString());
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }
            return parseJob;
        }
        public static async Task<JobStartResponseModel> StartJob(string serverAddress, JobStartRequestModel model)
        {
            JobStartResponseModel startJob = null;
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            var json = JsonConvert.SerializeObject(model);
            var result = await uitylity.PostNeverBounce(serverAddress + jobStart, json);
            if (result.Status == "success")
            {
                startJob = JsonConvert.DeserializeObject<JobStartResponseModel>(result.Data.ToString());
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }
            return startJob;
        }
        public static async Task<JobStatusResponseModel> JobStatus(string serverAddress, JobStatusRequestModel model)
        {

            JobStatusResponseModel statusJob = null;
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            var json = JsonConvert.SerializeObject(model);
            var result = await uitylity.GetNeverBounce(serverAddress + jobStatus, GenerateQuerstring(json));
            if (result.Status == "success")
            {
                statusJob = JsonConvert.DeserializeObject<JobStatusResponseModel>(result.Data.ToString());
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }
            return statusJob;
        }
        public static async Task<JobResultsResponseModel> JobResults(string serverAddress, JobResultsRequestModel model)
        {
            JobResultsResponseModel jobResults = null;
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            var json = ConvertResultDictionary(model);
            var result = await uitylity.GetNeverBounce(serverAddress + jobResult, GenerateQuerstring(json));
            if (result.Status == "success")
            {
                jobResults = JsonConvert.DeserializeObject<JobResultsResponseModel>(result.Data.ToString());
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }
            return jobResults;
        }
        public static async Task<JobDeleteResponseModel> DeleteJobs(string serverAddress, JobDeleteRequestModel model)
        {

            JobDeleteResponseModel deleteJob = null;
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            var json = JsonConvert.SerializeObject(model);
            var result = await uitylity.PostNeverBounce(serverAddress + jobDelete, json);
            if (result.Status == "success")
            {
                deleteJob = JsonConvert.DeserializeObject<JobDeleteResponseModel>(result.ToString());
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }

            return deleteJob;
        }
        public static async Task<String> DownloadJobData(string serverAddress, JobDownloadRequestModel model)
        {
            string downloadJob = null;
            SDKUtility uitylity = new Utilities.SDKUtility(serverAddress);
            var json = JsonConvert.SerializeObject(model);
            var result = await uitylity.PostNeverBouncedownload(serverAddress + jobDownload, json);
            if (result.Status == "success")
            {
                downloadJob = result.downloadResponse;
            }
            else
            {
                throw new Exception(result.Data.ToString());
            }
            return downloadJob;

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
            if (model.job_status != null && model.job_status != "")
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
            string str = "?";
            str += parameters.Replace(":", "=").Replace("{", "").
                        Replace("}", "").Replace(",", "&").
                            Replace("\"", "");

            return str;
        }



    }
}
