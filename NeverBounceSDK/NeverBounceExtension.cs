namespace Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NeverBounce;
using NeverBounce.Utilities;
using System;
using System.Net.Http;
using System.Reflection;

public static class NeverBounceExtension
{
    const string DEFAULT_HOST = "https://api.neverbounce.com/";
    const string DEFAULT_VERSION = "v4.2/";

    /// <summary>Add service for checking emails with https://neverbounce.com/</summary>
    /// <param name="services">Extended services collection</param>
    /// <param name="key">API key to add to all requests</param>
    /// <param name="host">Optional host if not https://api.neverbounce.com/</param>
    /// <param name="version">Optional version if not 4.2 (versions prior to 4.0 not supported)</param>
    /// <returns>Extended services collection</returns>
    public static IServiceCollection AddNeverBounceService(this IServiceCollection services, string key, string? host = null, string? version = null)
    {
        if (string.IsNullOrEmpty(key)) return services;

        // Get the base URL for every request
        var url = GetUrl(host, version);

        // Create a named client
        services.AddHttpClient("NeverBounce", client => ConfigureHttpClient(client, url));

        // Use the named client in a transient service
        return services.AddTransient(s =>
        {
            var clientFactory = s.GetRequiredService<IHttpClientFactory>();
            var httpClient = clientFactory.CreateClient("NeverBounce");
            var endpoint = new HttpServiceEndpoint(httpClient);

            // Add Companies House API service
            return new NeverBounceService(endpoint, key, s.GetService<ILoggerFactory>());
        });
    }

    /// <summary>Add service for checking emails with https://neverbounce.com/
    /// <para>Add a config section to hold connection parameters, key is required, host and version are optional:
    /// <code>
    /// "NeverBounce": {
    ///     "key":     "secret_████████████████████████",
    ///     "version": "v4.2",
    ///     "host":    "https://api.neverbounce.com/"
    /// },
    /// </code></para></summary>
    /// <param name="services">Extended services collection.</param>
    /// <param name="configuration">Configuration settings to get values from.</param>
    /// <returns>Extended services collection</returns>
    public static IServiceCollection AddNeverBounceService(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("NeverBounce");
        if (!section.Exists()) return services;

        // Get the required API key
        string? key = section["key"];
        if (key is null) return services;

        return services.AddNeverBounceService(key, section["host"], section["version"]);
    }

    /// <summary>Get the base URL, replace nulls with defaults, force to format: <code>https://{host}/{version}/</code></summary>
    static Uri GetUrl(string? host, string? version)
    {
        string h = host ?? DEFAULT_HOST;
        if (!h.EndsWith("/")) h += "/";

        // Ensure URL starts with https://
        if (h.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            h = string.Concat("https://", h.AsSpan("http://".Length));
        else if (!h.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            h = "https://" + h;

        string v = version ?? DEFAULT_VERSION;
        if (!v.EndsWith("/")) v += "/";
        return new Uri(h + v);
    }

    static void ConfigureHttpClient(HttpClient client, Uri baseUrl) {
        // Set user agent
        client.DefaultRequestHeaders.Remove("User-Agent");
        client.DefaultRequestHeaders.Add("User-Agent", $"NeverBounce-NETClient/{Assembly.GetEntryAssembly()?.
            GetCustomAttribute<AssemblyInformationalVersionAttri‌bute>()?.Informationa‌lVersion ?? "0.0.0"}");

        // Set default 30s timeout
        client.Timeout = new TimeSpan(0, 0, 30);

        // Set the base URL
        client.BaseAddress = baseUrl;
    }
}
