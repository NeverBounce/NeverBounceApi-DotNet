namespace NeverBounce.Models;

public enum JobStatus
{
    /// <summary>The job has fallen into our Q/A review and requires action on our end. 
    /// <para>Read more: https://neverbounce.com/help/clean/what-is-my-list-under-a-qa-review</para></summary>
    UnderReview,

    /// <summary>The job has been queued, this is because we're either too busy or you have too many active jobs.</summary>
    Queued,

    /// <summary>The job failed, typically this is due to poorly formatted data.</summary>
    Failed,

    /// <summary>The job has completed verification and the results are ready for download.</summary>
    Complete,

    /// <summary>The job is currently either running a sample or full verification.</summary>
    Running,

    /// <summary>The job has been received and we are parsing the data for emails and duplicate records.</summary>
    Parsing,

    /// <summary>The job has not yet run and is waiting to be started.</summary>
    Waiting,

    /// <summary>The job has completed analysis and is waiting for further action.</summary>
    WaitingAnalyzed,

    /// <summary>The job is currently being uploaded to the system.</summary>
    Uploading,
}
