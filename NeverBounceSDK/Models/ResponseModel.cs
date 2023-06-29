namespace NeverBounce.Models;

public abstract class ResponseModel
{
    public string status { get; set; }
    public string message { get; set; }
    public int execution_time { get; set; }
}

public class RawResponseModel
{
    public object json { get; set; }
    public string plaintext { get; set; }
}

public class CreditsInfo
{
    public int paid_credits_used { get; set; }
    public int free_credits_used { get; set; }
    public int paid_credits_remaining { get; set; }
    public int free_credits_remaining { get; set; }
}

public class AddressInfo
{
    public string original_email { get; set; }
    public string normalized_email { get; set; }
    public string addr { get; set; }
    public string alias { get; set; }
    public string host { get; set; }
    public string fqdn { get; set; }
    public string domain { get; set; }
    public string subdomain { get; set; }
    public string tld { get; set; }
}