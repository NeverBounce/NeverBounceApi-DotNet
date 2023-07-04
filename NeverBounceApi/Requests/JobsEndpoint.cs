namespace NeverBounce.Api;
using NeverBounce;
using NeverBounce.Models;
using System.Text;

public static class JobsEndpoint
{
    public static async Task Search(NeverBounceService neverBounceService, int page)
    {
        var result = await neverBounceService.Jobs.Search(new() { Page = page, ItemsPerPage = 100 });

        Console.WriteLine($"Jobs: {result.TotalResults} on {page}/{result.TotalPages} pages");
        if (result.Results is null) return;

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

    public static async Task Status(NeverBounceService neverBounceService, int jobID)
    {
        var jobStatus = await neverBounceService.Jobs.Status(jobID);
        Console.WriteLine($"{jobStatus.ID} {jobStatus.Filename}");
        Console.WriteLine($"\t{jobStatus.Status}");

        if (jobStatus.Total is not null) {
            Console.WriteLine($"\tTotals:");
            Console.WriteLine($"\t\tProcessed {jobStatus.Total.Processed ?? 0}");

            if (jobStatus.Total.Valid > 0)
                Console.WriteLine($"\t\tValid {jobStatus.Total.Valid}");

            if (jobStatus.Total.Invalid > 0)
                Console.WriteLine($"\t\tInvalid {jobStatus.Total.Invalid}");

            if (jobStatus.Total.Invalid > 0)
                Console.WriteLine($"\t\tBillable {jobStatus.Total.Invalid}");

            if (jobStatus.Total.BadSyntax > 0)
                Console.WriteLine($"\t\tBad syntax {jobStatus.Total.BadSyntax}");

            if (jobStatus.Total.Catchall > 0)
                Console.WriteLine($"\t\tCatch-all {jobStatus.Total.Catchall}");

            if (jobStatus.Total.Disposable > 0)
                Console.WriteLine($"\t\tDisposable {jobStatus.Total.Disposable}");

            if (jobStatus.Total.Duplicates > 0)
                Console.WriteLine($"\t\tDuplicates {jobStatus.Total.Duplicates}");

            if (jobStatus.Total.Unknown > 0)
                Console.WriteLine($"\t\tUnknown {jobStatus.Total.Unknown}");
        }
    }

    public static async Task Results(NeverBounceService neverBounceService, int jobID, int page)
    {
        var jobResults = await neverBounceService.Jobs.Results(jobID, page, 100);
        Console.WriteLine($"Total results: {jobResults.TotalResults} on {page}/{jobResults.TotalPages} pages");

        if (jobResults.Results is not null)
            foreach (var r in jobResults.Results) {
                var v = r.Verification;
                if (v is not null)
                {
                    Console.WriteLine($"\tEmail: {v.AddressInfo?.OriginalEmail}");
                    Console.WriteLine($"\t\t{SingleEndpoint.ResultCodeDescription(v.Result)}");

                    if (v.Flags?.Any() ?? false)
                        Console.WriteLine($"\t\tFlags: {string.Join(", ", v.Flags)}");
                }
                else if(r.Data is not null) {
                    foreach(var pair in r.Data)
                        Console.WriteLine($"\t\t{pair.Key}: {pair.Value}");
                }
            }
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

    public static async Task Delete(NeverBounceService neverBounceService, int jobID)
    {
        await neverBounceService.Jobs.Delete(jobID);
        // if no exception then it should have worked
        Console.WriteLine($"Job {jobID} deleted");
    }
}
