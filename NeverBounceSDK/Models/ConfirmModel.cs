namespace NeverBounce.Models;

public class ConfirmResponseModel : ResponseModel
{
    public bool TokenConfirmed { get; set; } = false;
}

public record ConfirmRequestModel(string Email, string ConfirmationToken, string TransactionID, string Result);