namespace NeverBounce.Models;

public enum ResultCode
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
