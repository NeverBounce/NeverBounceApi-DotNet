using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeverBounce;
using System.Text;

// Bug workaround: https://developercommunity.visualstudio.com/content/problem/176587/unicode-characters-in-output-window.html
// and https://stackoverflow.com/questions/51483609/in-net-core-using-ilogger-how-do-i-log-unicode-chars
Console.OutputEncoding = Encoding.UTF8;

// Init .NET DI host
var builder = Host.CreateDefaultBuilder(args);

#if DEBUG
// For this example force a dev environment
builder.UseEnvironment(Environments.Development);
#endif

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
var info = await neverBounceService.Account();

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
