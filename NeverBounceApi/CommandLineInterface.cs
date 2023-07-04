namespace NeverBounce.Cli;
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

        return await rootCommand.InvokeAsync(args);
    }
}
