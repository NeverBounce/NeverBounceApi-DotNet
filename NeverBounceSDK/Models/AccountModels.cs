namespace NeverBounce.Models;

public class AccountInfoResponseModel : ResponseModel
{
    public CreditsInfo CreditsInfo { get; set; }

    public JobCounts JobCounts { get; set; }
}

public class JobCounts
{
    public int Completed { get; set; }

    public int UnderReview { get; set; }

    public int Queued { get; set; }

    public int Processing { get; set; }
}