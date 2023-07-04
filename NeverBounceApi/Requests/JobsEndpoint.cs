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
                Console.WriteLine($"\t{j.ID} {j.Filename} FAILED {j.FailureReason}");
            else
            {
                Console.WriteLine($"\t{j.ID} {j.Filename}: {j.JobStatus} {j.BounceEstimate}%");
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

    public static async Task Parse(NeverBounceService neverBounceService, int jobID)
    {
        var queue = await neverBounceService.Jobs.Parse(jobID);
        Console.WriteLine($"Parse started for {jobID}, queue {queue}");
    }

    public static async Task Start(NeverBounceService neverBounceService, int jobID, bool runSample = false)
    {
        var queue = await neverBounceService.Jobs.Start(jobID, runSample);
        Console.WriteLine($"{(runSample ? "Sample" : "Run")} started for {jobID}, queue {queue}");
    }

    public static async Task<JobStatusResponseModel> Status(NeverBounceService sdk)
    {
        return await sdk.Jobs.Status(290561);
    }

    public static async Task<JobResultsResponseModel> Results(NeverBounceService sdk)
    {
        return await sdk.Jobs.Results(290561);
    }

    public static async Task Download(NeverBounceService neverBounceService, int jobID, string? fileName)
    {
        await using var stream = await neverBounceService.Jobs.Download(jobID);

        using var reader = new StreamReader(stream, Encoding.UTF8);
        if (fileName is not null)
        {
            Console.WriteLine("Downloading result...");

            using var writer = new StreamWriter(fileName, false, Encoding.UTF8);
            while (!reader.EndOfStream)
            {
                string? line = await reader.ReadLineAsync();
                if (line is not null)
                    await writer.WriteLineAsync("\t" + line);
            }
            await writer.FlushAsync();

            Console.WriteLine($"\tSaved to {fileName}");
        }
        else {
            Console.WriteLine("Download result:");
            while (!reader.EndOfStream)
            {
                string? line = await reader.ReadLineAsync();
                if (line is not null)
                    Console.WriteLine("\t" + line);
            }
        }
    }

    public static async Task Delete(NeverBounceService sdk)
    {
        await sdk.Jobs.Delete(290561);
    }
}
