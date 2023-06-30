namespace NeverBounce.Models;

public class AccountInfoResponseModel : ResponseModel
{
    public CreditsInfo CreditsInfo { get; set; } = new CreditsInfo();

    public JobCounts JobCounts { get; set; } = new JobCounts();
}
