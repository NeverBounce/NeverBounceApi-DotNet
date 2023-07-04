﻿namespace NeverBounce.Cli;

using NeverBounce.Api;
using System.CommandLine;
using System.Threading.Tasks;

static class CommandLineInterface
{
    public static async Task<int> Parse(NeverBounceService neverBounceService, string[] args)
    {
        // See https://learn.microsoft.com/en-us/dotnet/standard/commandline/

        var rootCommand = new RootCommand("Command-line interface to access NeverBounce");

        {   // Get account info with "account"
            var accountCommand = new Command("account", "Account info method allow to programmatically check your account's balance and how many jobs are currently running on your account.");
            accountCommand.AddAlias("info"); // or "check support@neverbounce.com"
            accountCommand.SetHandler(async () => await AccountEndpoint.Info(neverBounceService));
            rootCommand.Add(accountCommand);
        }

        {   // Check a single email with "single support@neverbounce.com"
            var checkCommand = new Command("single", "Single verification allows you verify individual emails and gather additional information pertaining to the email.");
            checkCommand.AddAlias("check"); // or "check support@neverbounce.com"
            var emailArgument = new Argument<string>("email", "The email to check.");
            checkCommand.AddArgument(emailArgument);
            checkCommand.SetHandler(async email => await SingleEndpoint.Check(neverBounceService, email), emailArgument);
            rootCommand.Add(checkCommand);
        }

        {
            // The bulk endpoint provides high-speed validation on a list of email addresses.
            var jobCommand = new Command("jobs", "List or find current jobs");

            // Default option is to search jobs
            var pageOption = new Option<int>("--page", () => 1, "1-indexed page to show, jobs shown 100 per page");
            jobCommand.AddOption(pageOption);
            jobCommand.SetHandler(async page => await JobsEndpoint.Search(neverBounceService, page), pageOption);

            {   // Create uploads a file or points to a file online
                var createCommand = new Command("create", "Create a batch to verify multiple emails together, the same way you would verify lists in the dashboard.");
                var fileArgument = new Argument<string>("file", "The file to upload or set");
                createCommand.AddArgument(fileArgument);
                createCommand.SetHandler(async file => await JobsEndpoint.Create(neverBounceService, file), fileArgument);
                jobCommand.Add(createCommand);
            }

            {   // Download the CSV data for the job
                var downloadCommand = new Command("download", "Download the CSV data for the job.");
                var jobArgument = new Argument<int>("job", "The ID of the job to download");
                downloadCommand.AddArgument(jobArgument);
                var fileOption = new Option<string>("--file", "Optional file to write the results to, if not passed output will be the console.");
                downloadCommand.AddOption(fileOption);
                downloadCommand.SetHandler(async (job, file) => await JobsEndpoint.Download(neverBounceService, job, file), jobArgument, fileOption);
                jobCommand.Add(downloadCommand);
            }

            {   // Parse a batch job that has been created with auto_parse disabled
                var parseCommand = new Command("parse", "Parse a job created with auto_parse disabled.");
                var jobArgument = new Argument<int>("job", "The ID of the job to parse");
                parseCommand.AddArgument(jobArgument);
                parseCommand.SetHandler(async job => await JobsEndpoint.Parse(neverBounceService, job), jobArgument);
                jobCommand.Add(parseCommand);
            }

            {   // Start a batch job that has been created with auto_start disabled
                var startCommand = new Command("start", "Start a job created with auto_start disabled.");
                var jobArgument = new Argument<int>("job", "The ID of the job to start");
                startCommand.AddArgument(jobArgument);
                var runSampleOption = new Option<bool>("--run-sample", "Should this job be run as a sample?");
                startCommand.AddOption(runSampleOption);
                startCommand.SetHandler(async (job, runSample) => await JobsEndpoint.Start(neverBounceService, job, runSample), jobArgument, runSampleOption);
                jobCommand.Add(startCommand);
            }

            {   // Parse a batch job that has been created with auto_parse disabled
                var statusCommand = new Command("status", "Get the current state of a job");
                var jobArgument = new Argument<int>("job", "The ID of the job to get the state of");
                statusCommand.AddArgument(jobArgument);
                statusCommand.SetHandler(async job => await JobsEndpoint.Status(neverBounceService, job), jobArgument);
                jobCommand.Add(statusCommand);
            }

            rootCommand.Add(jobCommand);
        }

        return await rootCommand.InvokeAsync(args);
    }
}
