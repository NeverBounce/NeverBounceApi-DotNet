namespace NeverBounce.Models;

/// <summary>Request object to serialise and send to the /job/results endpoint</summary>
public class JobResultsRequestModel : JobRequestModel
{
    public JobResultsRequestModel(int jobID) : base(jobID) { }

    /// <summary>The page to return the results from</summary>
    public int Page { get; set; } = 1;

    /// <summary>The number of results to be returned, between 1 and 1000</summary>
    public int ItemsPerPage { get; set; } = 10;
}
