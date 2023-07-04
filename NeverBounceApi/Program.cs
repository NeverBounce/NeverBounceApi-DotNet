using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeverBounce;
using NeverBounce.Cli;
using NeverBounce.Exceptions;
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

// This does all the argument parsing and calls the endpoint utility methods
await CommandLineInterface.Parse(neverBounceService, args);
