namespace NeverBounce.Models;

public class JobStatusResponseModel : ResponseModel
{
    public int ID { get; set; }

    /// <summary>Job status will indicate what stage the job is currently in. 
    /// This will be the primary property you'll want to check to determine what can be done with the job.</summary>
    public JobStatus JobStatus { get; set; }

    /// <summary>This will be what's displayed in the dashboard when viewing this job</summary>
    public string? Filename { get; set; }

    public string? CreatedAt { get; set; }

    public string? StartedAt { get; set; }

    public string? FinishedAt { get; set; }

    /// <summary>There are several items in the total object that give you an overview of verification results. 
    /// These numbers are updated periodically during the verification process.</summary>
    public JobStatusTotals? Total { get; set; }

    /// <summary>This property indicates the bounce rate we estimate the list to bounce at if sent to in its entirety. 
    /// The value of this property will be a float between 0.0 and 100.0.</summary>
    public float BounceEstimate { get; set; }

    /// <summary>This property indicates the overall progress of this job's verification. 
    /// The value of this property will be a float between 0.0 and 100.0.</summary>
    public float PercentComplete { get; set; }

    /// <summary>If the job has failed this will hold the reason, if known.</summary>
    public FailReason? FailureReason { get; set; }
}