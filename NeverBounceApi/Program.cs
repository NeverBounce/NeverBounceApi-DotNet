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

try
{
    // This does all the argument parsing and calls the endpoint utility methods
    // Any exceptions they throw can be handled here
    await CommandLineInterface.Parse(neverBounceService, args);

    // We only handle expected exceptions here:
}
catch (NeverBounceParseException parseX) {
    Console.Error.WriteLine("Exception parsing JSON response:");
    Console.Error.WriteLine(parseX.Message);
}
catch (NeverBounceResponseException httpX)
{
    Console.Error.WriteLine("Unhandled HTTP status code:");
    Console.Error.WriteLine($"HTTP Status code: {httpX.Status}");
    Console.Error.WriteLine(httpX.Message);
}
catch (NeverBounceServiceException nbX)
{
    Console.Error.WriteLine("Error returned from NeverBounce service:");
    Console.Error.WriteLine($"HTTP Status code: {nbX.Reason}");
    Console.Error.WriteLine(nbX.Message);
}
