namespace NeverBounce.Cli;

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
            checkCommand.SetHandler(async (emailValue) => await SingleEndpoint.Check(neverBounceService, emailValue), emailArgument);
            rootCommand.Add(checkCommand);
        }

        {
            // The bulk endpoint provides high-speed validation on a list of email addresses.
            var jobCommand = new Command("jobs", "List or find current jobs");

            // Default option is to search jobs
            var pageOption = new Option<int>("--page", () => 1, "1-indexed page to show, jobs shown 100 per page");
            jobCommand.AddOption(pageOption);
            jobCommand.SetHandler(async (pageValue) => await JobsEndpoint.Search(neverBounceService, pageValue), pageOption);

            var createCommand = new Command("create", "Create a batch to verify multiple emails together, the same way you would verify lists in the dashboard.");
            var fileArgument = new Argument<string>("file", "The file to upload or set");
            createCommand.AddArgument(fileArgument);
            createCommand.SetHandler(async (fileValue) => await JobsEndpoint.Create(neverBounceService, fileValue), fileArgument);
            jobCommand.Add(createCommand);

            rootCommand.Add(jobCommand);
        }

        return await rootCommand.InvokeAsync(args);
    }
}
