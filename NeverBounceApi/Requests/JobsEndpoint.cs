using NeverBounce;
using NeverBounce.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NeverBounceSdkExamples.Requests;

public class JobsEndpoint
{
    public static async Task<JobSearchResponseModel> Search(NeverBounceService sdk)
    {
        return await sdk.Jobs.Search();
    }

    class ExampleSendModel : ICreateRequestInputRecord { 
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public static async Task<int> CreateSuppliedData(NeverBounceService sdk)
    {
        var data = new ExampleSendModel[] {
            new ExampleSendModel { ID = 3, Email = "support@neverbounce.com", Name = "Fred McValid" },
            new ExampleSendModel { ID = 4, Email = "invalid@neverbounce.com", Name = "Bob McInvalid" },
        };
        return await sdk.Jobs.Create(data, name: "Created From dotNET array", autoParse: true);
    }

    public static async Task<int> CreateRemoteUrl(NeverBounceService sdk)
    {
        return await sdk.Jobs.Create("https://example.com/file.csv", name: "Created From dotNET file.csv", autoParse: true);
    }

    public static async Task<string> Parse(NeverBounceService sdk)
    {
        return await sdk.Jobs.Parse(290561);
    }

    public static async Task<string> Start(NeverBounceService sdk)
    {
        return await sdk.Jobs.Start(290561);
    }

    public static async Task<JobStatusResponseModel> Status(NeverBounceService sdk)
    {
        return await sdk.Jobs.Status(290561);
    }

    public static async Task<JobResultsResponseModel> Results(NeverBounceService sdk)
    {
        return await sdk.Jobs.Results(290561);
    }

    public static async Task<string> Download(NeverBounceService sdk)
    {
        return await sdk.Jobs.Download(290561);
    }

    public static async Task Delete(NeverBounceService sdk)
    {
        await sdk.Jobs.Delete(290561);
    }
}
