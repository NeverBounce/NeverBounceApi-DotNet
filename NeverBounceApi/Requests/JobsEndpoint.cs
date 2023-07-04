namespace NeverBounce.Api;
using NeverBounce;
using NeverBounce.Models;
using System.Text;

public static class JobsEndpoint
{
    public static async Task Search(NeverBounceService neverBounceService, int page)
    {
        var result = await neverBounceService.Jobs.Search(new() { Page = page, ItemsPerPage = 100 });

        Console.WriteLine($"Jobs: {result.TotalResults} in {result.TotalPages} pages");
        foreach(var j in result.Results)
        {
            if (j.JobStatus == JobStatus.Failed)
                Console.WriteLine($"\t{j.Filename} FAILED {j.FailureReason}");
            else
            {
                Console.WriteLine($"\t{j.Filename}: {j.JobStatus} {j.BounceEstimate}%");
                Console.WriteLine($"\t\tBounce estimate {j.BounceEstimate}%");

                if(j.JobStatus != JobStatus.Complete)
                    Console.WriteLine($"\t\tProgress {j.PercentComplete}%");
            }
        }
        
    }

    public static async Task Create(NeverBounceService neverBounceService, string file) {
        int jobID;
        if (file.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
            file.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            jobID = await neverBounceService.Jobs.Create(file, file, autoParse: false, autoStart: false, runSample: true);
        else
            jobID = await neverBounceService.Jobs.Create(ParseCsvFile(file), file, autoParse: false, autoStart: false, runSample: true);

        Console.WriteLine($"Job created: {jobID}");
    }

    static IEnumerable<IEnumerable<object>> ParseCsvFile(string file) {
        // Brute force dumb CSV parser, example code only
        using var reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            if (line is not null)
                yield return line.Split(',').Select(s => s.Trim(' ', '"'));
        }
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
        await using var stream = await sdk.Jobs.Download(290561);
        using TextReader reader = new StreamReader(stream, Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }

    public static async Task Delete(NeverBounceService sdk)
    {
        await sdk.Jobs.Delete(290561);
    }
}
