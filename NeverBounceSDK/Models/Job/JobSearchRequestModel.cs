namespace NeverBounce.Models;

public class JobSearchRequestModel : RequestModel
{
    /// <summary>Filter jobs based on its ID</summary>
    public int? JobID { get; set; }

    /// <summary>Filter jobs based on the filename (exact match)</summary>
    public string? Filename { get; set; }

    /// <summary>Filter jobs by the job status</summary>
    public JobStatus? JobStatus { get; set; }

    /// <summary>The page to grab the jobs from</summary>
    public int Page { get; set; } = 1;

    /// <summary>The number of jobs to display</summary>
    public int ItemsPerPage { get; set; } = 10;
}
