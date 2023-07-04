namespace NeverBounce.Models;

/// <summary>Result expected from the /account/info endpoint</summary>
public class AccountInfoResponseModel : ResponseModel
{
    public CreditsInfo CreditsInfo { get; set; } = new CreditsInfo();

    public JobCounts JobCounts { get; set; } = new JobCounts();
}
