namespace NeverBounce.Models;

/// <summary>Response when calling /job/parse, /job/start, or /job/start with run_sample=true</summary>
public class JobQueueResponseModel : ResponseModel
{
    public string? QueueID { get; set; }
}
