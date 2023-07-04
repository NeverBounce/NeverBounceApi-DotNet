namespace NeverBounce.Models;

public class JobRequestModel
{
    public JobRequestModel(int jobID) { this.JobID = jobID; }

    /// <summary>The ID of the job to action (required)</summary>
    public int JobID { get; }
}