using NeverBounce;
using NeverBounce.Models;

namespace NeverBounceSdkExamples.Requests;

public class JobsEndpoint
{
    public static async Task<JobSearchResponseModel> Search(NeverBounceService sdk)
    {
        var model = new JobSearchRequestModel();
        model.JobID = 288025;
        return await sdk.Jobs.Search(model);
    }

    public static async Task<JobCreateResponseModel> CreateSuppliedData(NeverBounceService sdk)
    {
        var model = new JobCreateSuppliedDataRequestModel();
        model.Filename = "Created From dotNET.csv";
        model.AutoParse = true;
        model.AutoStart = false;
        var data = new List<object>();
        data.Add(new {id = "3", email = "support@neverbounce.com", name = "Fred McValid"});
        data.Add(new {id = "4", email = "invalid@neverbounce.com", name = "Bob McInvalid"});
        model.Input = data;
        return await sdk.Jobs.CreateFromSuppliedData(model);
    }

    public static async Task<JobCreateResponseModel> CreateRemoteUrl(NeverBounceService sdk)
    {
        var model = new JobCreateRemoteUrlRequestModel();
        model.Filename = "Created From dotNET.csv";
        model.AutoParse = true;
        model.AutoStart = false;
        model.Input = "https://example.com/file.csv";
        return await sdk.Jobs.CreateFromRemoteUrl(model);
    }

    public static async Task<JobParseResponseModel> Parse(NeverBounceService sdk)
    {
        var model = new JobParseRequestModel();
        model.JobID = 290561;
        return await sdk.Jobs.Parse(model);
    }

    public static async Task<JobStartResponseModel> Start(NeverBounceService sdk)
    {
        var model = new JobStartRequestModel();
        model.JobID = 290561;
        return await sdk.Jobs.Start(model);
    }

    public static async Task<JobStatusResponseModel> Status(NeverBounceService sdk)
    {
        var model = new JobStatusRequestModel();
        model.JobID = 290561;
        return await sdk.Jobs.Status(model);
    }

    public static async Task<JobResultsResponseModel> Results(NeverBounceService sdk)
    {
        var model = new JobResultsRequestModel();
        model.JobID = 290561;
        return await sdk.Jobs.Results(model);
    }

    public static async Task<string> Download(NeverBounceService sdk)
    {
        var model = new JobDownloadRequestModel();
        model.JobID = 290561;
        return await sdk.Jobs.Download(model);
    }

    public static async Task<JobDeleteResponseModel> Delete(NeverBounceService sdk)
    {
        var model = new JobDeleteRequestModel();
        model.JobID = 290561;
        return await sdk.Jobs.Delete(model);
    }
}
