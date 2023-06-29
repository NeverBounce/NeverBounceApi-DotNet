using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests
{
    public class JobsEndpoint
    {
        public static async Task<JobSearchResponseModel> Search(NeverBounceService sdk)
        {
            var model = new JobSearchRequestModel();
            model.job_id = 288025;
            return await sdk.Jobs.Search(model);
        }

        public static async Task<JobCreateResponseModel> CreateSuppliedData(NeverBounceService sdk)
        {
            var model = new JobCreateSuppliedDataRequestModel();
            model.filename = "Created From dotNET.csv";
            model.auto_parse = true;
            model.auto_start = false;
            var data = new List<object>();
            data.Add(new {id = "3", email = "support@neverbounce.com", name = "Fred McValid"});
            data.Add(new {id = "4", email = "invalid@neverbounce.com", name = "Bob McInvalid"});
            model.input = data;
            return await sdk.Jobs.CreateFromSuppliedData(model);
        }

        public static async Task<JobCreateResponseModel> CreateRemoteUrl(NeverBounceService sdk)
        {
            var model = new JobCreateRemoteUrlRequestModel();
            model.filename = "Created From dotNET.csv";
            model.auto_parse = true;
            model.auto_start = false;
            model.input = "https://example.com/file.csv";
            return await sdk.Jobs.CreateFromRemoteUrl(model);
        }

        public static async Task<JobParseResponseModel> Parse(NeverBounceService sdk)
        {
            var model = new JobParseRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Parse(model);
        }

        public static async Task<JobStartResponseModel> Start(NeverBounceService sdk)
        {
            var model = new JobStartRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Start(model);
        }

        public static async Task<JobStatusResponseModel> Status(NeverBounceService sdk)
        {
            var model = new JobStatusRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Status(model);
        }

        public static async Task<JobResultsResponseModel> Results(NeverBounceService sdk)
        {
            var model = new JobResultsRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Results(model);
        }

        public static async Task<string> Download(NeverBounceService sdk)
        {
            var model = new JobDownloadRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Download(model);
        }

        public static async Task<JobDeleteResponseModel> Delete(NeverBounceService sdk)
        {
            var model = new JobDeleteRequestModel();
            model.job_id = 290561;
            return await sdk.Jobs.Delete(model);
        }
    }
}
