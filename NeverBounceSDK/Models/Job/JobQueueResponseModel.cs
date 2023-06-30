namespace NeverBounce.Models;

/// <summary>Response when calling /parse, /start, or /start with run_sample</summary>
public class JobQueueResponseModel : ResponseModel
{
    public string? QueueID { get; set; }
}
