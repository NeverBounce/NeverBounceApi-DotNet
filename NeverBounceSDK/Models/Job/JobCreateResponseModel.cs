namespace NeverBounce.Models;

/// <summary>Result expected from the /job/create endpoint</summary>
public class JobCreateResponseModel : ResponseModel
{
    public int JobID { get; set; }
}
