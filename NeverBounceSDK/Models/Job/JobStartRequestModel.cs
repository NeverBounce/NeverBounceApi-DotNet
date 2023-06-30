namespace NeverBounce.Models;

public class JobStartRequestModel : JobRequestModel
{
    public JobStartRequestModel(int jobID) : base(jobID) { }

    /// <summary>Should this job be run as a sample? (default: false)</summary>
    public bool RunSample { get; set; } = false;
}

