namespace NeverBounce.Models;

/// <summary>Request object to serialise and send to the /job/parse endpoint</summary>
public class JobParseRequestModel : JobRequestModel
{
    public JobParseRequestModel(int jobID) : base(jobID) { }

    /// <summary>Should the job start processing immediately after it's parsed? (default: false)</summary>
    public bool AutoStart { get; set; } = false;
}