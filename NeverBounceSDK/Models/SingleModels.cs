namespace NeverBounce.Models;

public enum ResultCodes
{
    /// <summary>A valid email address has been verified as a real email that is currently accepting mail.</summary>
    Valid,

    /// <summary>An invalid email address has been verified as a bad recipient address that does not exist or is not accepting mail. 
    /// Invalid emails will result in a bounce.</summary>
    Invalid,

    /// <summary>Disposable emails are temporary accounts used to avoid using a real personal account during a sign-up process. 
    /// Common providers of disposable emails include Mailinator, Guerilla Mail, AirMail, and 10 Minute Mail.</summary>
    Disposable,

    /// <summary>This is also known as an “catch all”. 
    /// This is a domain-wide setting where all emails on this domain will be reported as an "accept all". 
    /// There is no definitive way to determine whether this email is valid or invalid.
    /// <para>An accept all address is commonly used in small businesses to ensure a company receives any email that has been sent to them, regardless of typos. 
    /// Additionally, these are also found in larger government, medical and educational organizations. 
    /// Oftentimes these are infact valid emails. 
    /// However some organizations may utilize this setting as a security feature to prevent unsolicited emails.</para></summary>
    Catchall,

    /// <summary>We are unable to definitively determine this email’s status. 
    /// This email appears to be OK, however the domain and/or server is not responding to our requests. 
    /// This may be due to an issue with their internal network or expired domain names. 
    /// Unknown addresses are checked up to 75 times before this result code is given.</summary>
    Unknown,
}

public enum ResultFlag
{
    ///<summary>The input has one or more DNS records associated with the hostname.</summary>
    HasDns,

    ///<summary>The input has mail exchanger DNS records configured.</summary>
    HasDnsMX,

    ///<summary>The input given doesn't appear to be an email.</summary>
    BadSyntax,

    ///<summary>This email is registered on a free-mail host. (e.g: yahoo.com, hotmail.com)</summary>
    FreeEmailHost,

    Profanity,

    ///<summary>This email is a role-based email address (e.g: admin@, help@, sales@)</summary>
    RoleAccount,

    ///<summary>The input given is a disposable email.</summary>
    DisposableEmail,

    ///<summary>The input given is a government email.</summary>
    GovernmentHost,

    ///<summary>The input given is a acedemic email.</summary>
    AcademicHost,

    ///<summary>The input given is a military email.</summary>
    MilitaryHost,

    ///<summary>INT designated domain names.</summary>
    InternationalHost,

    ///<summary>Host likely intended to look like a big-time provider (type of spam trap).</summary>
    SquatterHost,

    ///<summary>The input was misspelled</summary>
    SpellingMistake,

    BadDns,

    TemporaryDnsError,

    ///<summary>Unable to connect to remote host.</summary>
    ConnectFails,

    ///<summary>Remote host accepts mail at any address.</summary>
    AcceptsAll,

    ///<summary>The email address supplied contains an address part and an alias part.</summary>
    SontainsAlias,

    ///<summary>The host in the address contained a subdomain.</summary>
    SontainsSubdomain,

    ///<summary>We were able to connect to the remote mail server.</summary>
    SmtpConnectable,

    ///<summary>Host is affiliated with a known spam trap network.</summary>
    SpamtrapNetwork,

    ///<summary>Indicates the result was generated using the historical-driven algorithm</summary>
    HistoricalResponse,
}

public class SingleResponseModel : ResponseModel
{
    /// <summary>Result of the email evaluation</summary>
    public ResultCodes Result { get; set; }

    public IEnumerable<ResultFlag>? Flags { get; set; }

    /// <summary>These are soft suggestions that may correct common typos such as "gmal.com" or "hotmal.com".</summary>
    public string? SuggestedCorrection { get; set; }

    public string? RetryToken { get; set; }

    public CreditsInfo CreditsInfo { get; set; }

    public AddressInfo AddressInfo { get; set; }
}

public class SingleRequestModel : RequestModel
{
    public SingleRequestModel(string email) { this.Email = email; }

    /// <summary>The email to verify, required</summary>
    public string Email { get; }

    /// <summary>Include additional address info in response (default: false)</summary>
    public bool? AddressInfo { get; set; } = false;

    /// <summary>Include account credit info in response (default: false)</summary>
    public bool? CreditsInfo { get; set; } = false;

    /// <summary>The maximum time in seconds we should try to verify the address</summary>
    public int? Timeout { get; set; }

    /// <summary>Miscellanious request meta data</summary>
    public RequestMetaDataModel RequestMetaData { get; set; } = new RequestMetaDataModel();
}
