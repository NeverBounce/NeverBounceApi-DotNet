namespace NeverBounce.Models;

public class EmailAddressInfo
{
    public string? OriginalEmail { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? Addr { get; set; }

    public string? Alias { get; set; }

    public string? Host { get; set; }

    public string? Fqdn { get; set; }

    public string? Domain { get; set; }

    public string? Subdomain { get; set; }

    public string? Tld { get; set; }
}
