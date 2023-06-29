namespace NeverBounce.Models;

public class POEConfirmResponseModel : ResponseModel
{
    public bool token_confirmed { get; set; }
}

public class POEConfirmRequestModel : RequestModel
{
    public string email { get; set; }
    public string confirmation_token { get; set; }
    public string transaction_id { get; set; }
    public string result { get; set; }
}