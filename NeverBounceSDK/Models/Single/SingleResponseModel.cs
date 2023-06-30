namespace NeverBounce.Models;

public class SingleResponseModel : ResponseModel
{
    /// <summary>Result of the email evaluation</summary>
    public ResultCode Result { get; set; }

    /// <summary>The flags will give you additional information that we discovered about the domain during verification. 
    /// <para>See https://developers.neverbounce.com/reference/single-check#flags</para></summary>
    public IEnumerable<string>? Flags { get; set; }

    /// <summary>These are soft suggestions that may correct common typos such as "gmal.com" or "hotmal.com".</summary>
    public string? SuggestedCorrection { get; set; }

    public string? RetryToken { get; set; }

    public CreditsInfo CreditsInfo { get; set; }

    public EmailAddressInfo AddressInfo { get; set; }
}

