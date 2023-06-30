namespace NeverBounce.Models;

public class JobSearchResponseModel : ResponseModel
{
    public int TotalResults { get; set; }

    public int TotalPages { get; set; }

    public JobSearchQuery Query { get; set; }

    public IEnumerable<JobStatusResponseModel> Results { get; set; }
}

public class JobSearchQuery
{
    public int Page { get; set; } = 1;

    public int ItemsPerPage { get; set; } = 10;

    public int JobID { get; set; }
}