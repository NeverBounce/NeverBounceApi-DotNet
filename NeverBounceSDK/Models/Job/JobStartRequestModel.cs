namespace NeverBounce.Models;

/// <summary>Request object to serialise and send to the /job/start endpoint</summary>
public class JobStartRequestModel : JobRequestModel
{
    public JobStartRequestModel(int jobID) : base(jobID) { }

    /// <summary>Should this job be run as a sample? (default: false)</summary>
    public bool RunSample { get; set; } = false;
}

