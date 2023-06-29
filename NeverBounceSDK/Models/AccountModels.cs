namespace NeverBounce.Models
{
    public class AccountInfoResponseModel : ResponseModel
    {
        public CreditsInfo credits_info { get; set; }
        public JobCounts job_counts { get; set; }
    }

    public class JobCounts
    {
        public int completed { get; set; }
        public int under_review { get; set; }
        public int queued { get; set; }
        public int processing { get; set; }
    }
}