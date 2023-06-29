namespace NeverBounce.Models;

public class JobSearchRequestModel : RequestModel
{
    public int? JobID{ get; set; }
    public string Filename { get; set; }
    public string JobStatus { get; set; }
    public int Page { get; set; } = 1;
    public int ItemsPerPage { get; set; } = 10;
}

public class JobSearchResponseModel : ResponseModel
{
    public int TotalResults { get; set; }
    public int TotalPages { get; set; }
    public JobSearchQuery Query { get; set; }
    public List<JobStatusResult> Results { get; set; }
}

public class JobSearchQuery
{
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
    public int JobID { get; set; }
}

public class JobStatusResult
{
    public int ID { get; set; }
    public string JobStatus { get; set; }
    public string Filename { get; set; }
    public string CreatedAt { get; set; }
    public object StartedAt { get; set; }
    public object FinishedAt { get; set; }
    public JobsTotals Total { get; set; }
    public float BounceEstimate { get; set; }
    public float PercentComplete { get; set; }
}

public class JobsTotals
{
    public int? Records { get; set; }
    public int? Billable { get; set; }
    public int? Processed { get; set; }
    public int? Valid { get; set; }
    public int? Invalid { get; set; }
    public int? Catchall { get; set; }
    public int? Disposable { get; set; }
    public int? Unknown { get; set; }
    public int? Duplicates { get; set; }
    public int? BadSyntax { get; set; }
}

public class JobCreateRequestModel : RequestModel
{
    public string Filename { get; set; }
    public bool AutoStart { get; set; } = false;
    public bool AutoParse { get; set; } = false;
    public bool RunSample { get; set; } = false;
    public bool AllowManualReview { get; set; } = false;
    public string CallbackUrl { get; set; }
    public Dictionary<string, string> CallbackHeaders { get; set; }
    public RequestMetaDataModel RequestMetaData { get; set; } = new RequestMetaDataModel();
}

public class JobCreateSuppliedDataRequestModel : JobCreateRequestModel
{
    public string InputLocation { get; } = "supplied";
    public List<object> Input { get; set; }
}

public class JobCreateRemoteUrlRequestModel : JobCreateRequestModel
{
    public string InputLocation { get; } = "remote_url";
    public string Input { get; set; }
}

public class JobCreateResponseModel : ResponseModel
{
    public int JobID { get; set; }
}

public class JobParseRequestModel : RequestModel
{
    public int JobID { get; set; }
    public bool AutoStart { get; set; } = false;
}

public class JobParseResponseModel : ResponseModel
{
    public string QueueID { get; set; }
}

public class JobStartRequestModel : RequestModel
{
    public int JobID { get; set; }
    public bool RunSample { get; set; }
    public bool AllowManualReview { get; set; } = false;
}

public class JobStartResponseModel : ResponseModel
{
    public string QueueID { get; set; }
}

public class JobStatusRequestModel : RequestModel
{
    public int JobID { get; set; }
}

public class JobStatusResponseModel : ResponseModel
{
    public int ID { get; set; }
    public string JobStatus { get; set; }
    public string Filename { get; set; }
    public string CreatedAt { get; set; }
    public object StartedAt { get; set; }
    public object FinishedAt { get; set; }
    public JobsTotals Total { get; set; }
    public float BounceEstimate { get; set; }
    public float PercentComplete { get; set; }

    public float FailureReason { get; set; }
}

public class JobResultsRequestModel : RequestModel
{
    public int JobID { get; set; }
    public int Page { get; set; } = 1;
    public int ItemsPerPage { get; set; } = 10;
}

public class JobResultsResponseModel : ResponseModel
{
    public int TotalResults { get; set; }
    public int TotalPages { get; set; }
    public JobsResultsQuery Query { get; set; }
    public List<Result> Results { get; set; }
}

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

public class Result
{
    public Dictionary<string, object> Data { get; set; }
    public SingleResponseModel Verification { get; set; }
}

public class JobDownloadRequestModel : RequestModel
{
    public int JobID { get; set; }
    public bool Valids { get; set; } = true;
    public bool Invalids { get; set; } = true;
    public bool Catchalls { get; set; } = true;
    public bool Unknowns { get; set; } = true;
    public bool Disposables { get; set; } = true;
}

public class JobDeleteRequestModel : RequestModel
{
    public int JobID { get; set; }
}

public class JobDeleteResponseModel : ResponseModel
{
}
