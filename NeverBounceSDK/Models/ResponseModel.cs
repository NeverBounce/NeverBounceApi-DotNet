namespace NeverBounce.Models;

public enum ResponseStatus {
    None = 0,
    Success,
    AuthFailure,
    TempUnavail,
    ThrottleTriggered,
    BadReferrer,
}

public abstract class ResponseModel
{
    public ResponseStatus Status { get; set; } = ResponseStatus.None;

    public string? Message { get; set; } = null;

    public int ExecutionTime { get; set; }
}

public class RawResponseModel
{
    public object Json { get; set; }
    public string Plaintext { get; set; }
}

public class CreditsInfo
{
    public int PaidCreditsUsed { get; set; }
    public int FreeCreditsUsed { get; set; }
    public int PaidCreditsRemaining { get; set; }
    public int FreeCreditsRemaining { get; set; }
}

public class AddressInfo
{
    public string OriginalEmail { get; set; }
    public string NormalizedEmail { get; set; }
    public string Addr { get; set; }
    public string Alias { get; set; }
    public string Host { get; set; }
    public string Fqdn { get; set; }
    public string Domain { get; set; }
    public string Subdomain { get; set; }
    public string Tld { get; set; }
}
