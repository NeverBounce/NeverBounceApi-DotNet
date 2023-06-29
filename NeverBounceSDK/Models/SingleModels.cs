namespace NeverBounce.Models;

public enum ResultCodes
{
    valid,
    invalid,
    disposable,
    catchall,
    unknown
}

public class SingleResponseModel : ResponseModel
{
    public string Result { get; set; }
    public List<string> Flags { get; set; }
    public string SuggestedCorrection { get; set; }
    public string RetryToken { get; set; }
    public CreditsInfo CreditsInfo { get; set; }
    public AddressInfo AddressInfo { get; set; }

    public bool ResultIs(string resultCode)
    {
        return this.Result.ToLower() == resultCode.ToLower();
    }

    public bool ResultIs(IEnumerable<string> resultCodes)
    {
        resultCodes = resultCodes.Select(c => c.ToLower());
        return resultCodes.Contains(this.Result.ToLower());
    }

    public bool ResultIsNot(string resultCode)
    {
        return this.Result.ToLower() != resultCode.ToLower();
    }

    public bool ResultIsNot(IEnumerable<string> resultCodes)
    {
        resultCodes = resultCodes.Select(c => c.ToLower());
        return !resultCodes.Contains(this.Result.ToLower());
    }
}

public class SingleRequestModel : RequestModel
{
    public string Email { get; set; }
    public bool? AddressInfo { get; set; } = false;
    public bool? CreditsInfo { get; set; } = false;
    public int? Timeout { get; set; }
    public RequestMetaDataModel RequestMetaData { get; set; } = new RequestMetaDataModel();
}
