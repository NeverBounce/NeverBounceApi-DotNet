using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Models
{
    //public class AccountInfoModel
    //{
    //    public string billing_type { get; set; }
    //    public int credits { get; set; }
    //    public int free_api_credits { get; set; }
    //    public int monthly_api_usage { get; set; }
    //    public int monthly_dashboard_usage { get; set; }
    //    public int jobs_completed { get; set; }
    //    public int jobs_under_review { get; set; }
    //    public int jobs_queued { get; set; }
    //    public int jobs_processing { get; set; }
    //    public int execution_time { get; set; }
    //    public string app_key { get; set; }


    //}

    public class CreditsInfo
    {
        public int paid_credits_used { get; set; }
        public int free_credits_used { get; set; }
        public int paid_credits_remaining { get; set; }
        public int free_credits_remaining { get; set; }
    }

    public class JobCounts
    {
        public int completed { get; set; }
        public int under_review { get; set; }
        public int queued { get; set; }
        public int processing { get; set; }
    }

    public class AccountInfoModel
    {
        public string status { get; set; }
        public CreditsInfo credits_info { get; set; }
        public JobCounts job_counts { get; set; }
        public int execution_time { get; set; }
    }
}
