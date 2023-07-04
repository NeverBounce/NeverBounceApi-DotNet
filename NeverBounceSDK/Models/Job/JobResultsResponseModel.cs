namespace NeverBounce.Models;

/// <summary>Result expected from the /job/results endpoint</summary>
public class JobResultsResponseModel : ResponseModel
{
    public int TotalResults { get; set; }

    public int TotalPages { get; set; }

    public JobsResultsQuery? Query { get; set; }

    public IEnumerable<JobResultsResponseRecord>? Results { get; set; }
}

/// <summary>Results totals for <see cref="JobResultsResponseModel"/></summary>
public class JobsResultsQuery
{
    public int JobID { get; set; }

    public int Valids { get; set; }

    public int Invalids { get; set; }

    public int Disposables { get; set; }

    public int Catchalls { get; set; }

    public int Unknowns { get; set; }

    public int Page { get; set; }

    public int ItemsPerPage { get; set; }
}

public class JobResultsResponseRecord
{
    /// <summary>The data object will contain the original data submitted for this row. 
    /// If the source data was submitted via the API the data will have the same keys as when it was originally submitted. 
    /// If submitted as a CSV in the dashboard the data will use the header row to determine the keys for the data when available.</summary>
    public Dictionary<string, object>? Data { get; set; }

    /// <summary>The verification object contains the verification results for the email being verified. </summary>
    public SingleResponseModel? Verification { get; set; }
}
