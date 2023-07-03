using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeverBounce;

// Init .NET DI host
var builder = Host.CreateDefaultBuilder(args);

// Get config - local appsettings.json, user secrets (for API key, which shouldn't go into source control), environment variables
builder.ConfigureHostConfiguration(host => host.
    AddJsonFile("appsettings.json", optional: false).
    AddUserSecrets("neverbounce").
    AddEnvironmentVariables());

// Add service with configuration
builder.ConfigureServices((host, s) => s.AddNeverBounceService(host.Configuration));

// Build the host
using var host = builder.Build();
await host.StartAsync();

// Get the never bounce service from DI
// In another DI service you can just add a NeverBounceService parameter to the constructor
// Or in a controller you can add [FromServices] attribute
var neverBounceService = host.Services.GetRequiredService<NeverBounceService>();

// Call the account info endpoint
var info = await neverBounceService.Account.Info();

var c = info.CreditsInfo;
Console.WriteLine($"Free credits: remaining {c?.FreeCreditsRemaining ?? 0}, used {c?.FreeCreditsUsed ?? 0}");
Console.WriteLine($"Paid credits: remaining {c?.PaidCreditsRemaining ?? 0}, used {c?.PaidCreditsUsed ?? 0}");

var j = info.JobCounts;
Console.WriteLine($"Jobs: pending {j?.Queued ?? 0}, processing {j?.Processing ?? 0}, completed {j?.Completed ?? 0}, under review {j?.UnderReview ?? 0}");

Console.ReadLine();

        
// var response = AccountEndpoint.Info(sdk).Result;
//var response = POEEndpoints.Confirm(sdk).Result;
//var response = SingleEndpoints.Check(sdk).Result;
//var response = JobsEndpoint.Search(sdk).Result;
//var response = JobsEndpoint.CreateSuppliedData(sdk).Result;
//var response = JobsEndpoint.CreateRemoteUrl(sdk).Result;
//var response = JobsEndpoint.Parse(sdk).Result;
//var response = JobsEndpoint.Start(sdk).Result;
//var response = JobsEndpoint.Status(sdk).Result;
//var response = JobsEndpoint.Results(sdk).Result;
//var response = JobsEndpoint.Download(sdk).Result;
//var response = JobsEndpoint.Delete(sdk).Result;
