namespace NeverBounce.Models;

public class JobSearchRequestModel : RequestModel
{
    public int? job_id { get; set; }
    public string filename { get; set; }
    public string job_status { get; set; }
    public int page { get; set; } = 1;
    public int items_per_page { get; set; } = 10;
}

public class JobSearchResponseModel : ResponseModel
{
    public int total_results { get; set; }
    public int total_pages { get; set; }
    public JobSearchQuery query { get; set; }
    public List<JobStatus> results { get; set; }
}

public class JobSearchQuery
{
    public int page { get; set; }
    public int items_per_page { get; set; }
    public int job_id { get; set; }
}

public class JobStatus
{
    public int id { get; set; }
    public string job_status { get; set; }
    public string filename { get; set; }
    public string created_at { get; set; }
    public object started_at { get; set; }
    public object finished_at { get; set; }
    public JobsTotals total { get; set; }
    public float bounce_estimate { get; set; }
    public float percent_complete { get; set; }
}

public class JobsTotals
{
    public int? records { get; set; }
    public int? billable { get; set; }
    public int? processed { get; set; }
    public int? valid { get; set; }
    public int? invalid { get; set; }
    public int? catchall { get; set; }
    public int? disposable { get; set; }
    public int? unknown { get; set; }
    public int? duplicates { get; set; }
    public int? bad_syntax { get; set; }
}

public class JobCreateRequestModel : RequestModel
{
    public string filename { get; set; }
    public bool auto_start { get; set; } = false;
    public bool auto_parse { get; set; } = false;
    public bool run_sample { get; set; } = false;
    public bool allow_manual_review { get; set; } = false;
    public string callback_url { get; set; }
    public Dictionary<string, string> callback_headers { get; set; }
    public RequestMetaDataModel request_meta_data { get; set; } = new RequestMetaDataModel();
}

public class JobCreateSuppliedDataRequestModel : JobCreateRequestModel
{
    public string input_location { get; } = "supplied";
    public List<object> input { get; set; }
}

public class JobCreateRemoteUrlRequestModel : JobCreateRequestModel
{
    public string input_location { get; } = "remote_url";
    public string input { get; set; }
}

public class JobCreateResponseModel : ResponseModel
{
    public int job_id { get; set; }
}

public class JobParseRequestModel : RequestModel
{
    public int job_id { get; set; }
    public bool auto_start { get; set; } = false;
}

public class JobParseResponseModel : ResponseModel
{
    public string queue_id { get; set; }
}

public class JobStartRequestModel : RequestModel
{
    public int job_id { get; set; }
    public bool run_sample { get; set; }
    public bool allow_manual_review { get; set; } = false;
}

public class JobStartResponseModel : ResponseModel
{
    public string queue_id { get; set; }
}

public class JobStatusRequestModel : RequestModel
{
    public int job_id { get; set; }
}

public class JobStatusResponseModel : ResponseModel
{
    public int id { get; set; }
    public string job_status { get; set; }
    public string filename { get; set; }
    public string created_at { get; set; }
    public string started_at { get; set; }
    public string finished_at { get; set; }
    public JobsTotals total { get; set; }
    public float bounce_estimate { get; set; }
    public float percent_complete { get; set; }
    public float failure_reason { get; set; }
}

public class JobResultsRequestModel : RequestModel
{
    public int job_id { get; set; }
    public int page { get; set; } = 1;
    public int items_per_page { get; set; } = 10;
}

public class JobResultsResponseModel : ResponseModel
{
    public int total_results { get; set; }
    public int total_pages { get; set; }
    public JobsResultsQuery query { get; set; }
    public List<Result> results { get; set; }
}

public class JobsResultsQuery
{
    public int job_id { get; set; }
    public int valids { get; set; }
    public int invalids { get; set; }
    public int disposables { get; set; }
    public int catchalls { get; set; }
    public int unknowns { get; set; }
    public int page { get; set; }
    public int items_per_page { get; set; }
}

public class Result
{
    public Dictionary<string, object> data { get; set; }
    public SingleResponseModel verification { get; set; }
}

public class JobDownloadRequestModel : RequestModel
{
    public int job_id { get; set; }
    public bool valids { get; set; } = true;
    public bool invalids { get; set; } = true;
    public bool catchalls { get; set; } = true;
    public bool unknowns { get; set; } = true;
    public bool disposables { get; set; } = true;
}

public class JobDeleteRequestModel : RequestModel
{
    public int job_id { get; set; }
}

public class JobDeleteResponseModel : ResponseModel
{
}
