namespace NeverBounce.Models;

public class POEConfirmResponseModel : ResponseModel
{
    public bool TokenConfirmed { get; set; }
}

public class POEConfirmRequestModel : RequestModel
{
    public string Email { get; set; }
    public string ConfirmationToken { get; set; }
    public string TransactionID { get; set; }
    public string Result { get; set; }
}