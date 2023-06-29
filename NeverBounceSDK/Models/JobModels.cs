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


public abstract class JobCreateRequestModel : RequestModel
{
    /// <summary>This will be what's displayed in the dashboard when viewing this job</summary>
    public string? Filename { get; set; }

    /// <summary>This will enable or disable the job from automatically beginning the verification process once it has been parsed. 
    /// If this is set to false you will need to call the /start endpoint to begin verification. 
    /// Setting this to true will start the job and deduct the credits. 
    /// Defaults to false</summary>
    public bool AutoStart { get; set; } = false;

    /// <summary>This will enable or disable the indexing process from automatically starting as soon as you create a job. 
    /// If set to false you will need to call the /parse endpoint after the job has been created to begin indexing. 
    /// Defaults to false</summary>
    public bool AutoParse { get; set; } = false;

    /// <summary>This endpoint has the ability to run a sample on the list and provide you with an estimated bounce rate without costing you. 
    /// Based on the estimated bounce rate you can decide whether or not to perform the full validation or not. 
    /// You can start validation by calling the /start endpoint.  
    /// Defaults to false.
    /// More: https://developers.neverbounce.com/docs/running-a-free-analysis</summary>
    public bool RunSample { get; set; } = false;

    /// <summary>Our manual review process allows us to take a second look at jobs that have a high rate of unknowns. 
    /// Often we can re-run lists at a later time or under specific configurations to further resolve these unknowns. 
    /// This requires a member of our team to perform these reviews so they only occur during our normal business hours.
    /// <para>While this works well for most dashboard users, this isn't always optimal for API users who programmatically access their lists. 
    /// Starting in 4.2 we've introduced the allow_manual_review to allow users to opt-into this featured when creating their API jobs.</para>
    /// <para>If a allow_manual_review is enabled, your job may take up to 1 business day to be released if it falls into the manual review queue.
    /// The job status will include the under_review job status if it's in this queue.</para></summary>
    public bool AllowManualReview { get; set; } = false;

    /// <summary>An optional URL that we should send events to during the lifecycle of the job</summary>
    public string CallbackUrl { get; set; }

    /// <summary>An optional array of headers that should be included when sending events to the callback_url</summary>
    public Dictionary<string, string>? CallbackHeaders { get; set; }

    /// <summary>Miscellanious request meta data</summary>
    public RequestMetaDataModel RequestMetaData { get; set; } = new RequestMetaDataModel();
}

public class JobCreateSuppliedDataRequestModel : JobCreateRequestModel
{
    public JobCreateSuppliedDataRequestModel(IEnumerable<ICreatRequestInputRecord> input) { this.Input = input; }

    /// <summary>This endpoint can receive input in multiple ways.</summary>
    public string InputLocation { get; } = "supplied";

    /// <summary>Supplying the data directly gives you the option to dynamically create email lists on the fly rather than having to write to a file.
    /// The key names will be used for the column headers.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    public IEnumerable<ICreatRequestInputRecord> Input { get; }
}

public class JobCreateSuppliedArrayRequestModel : JobCreateRequestModel
{
    public JobCreateSuppliedArrayRequestModel(IEnumerable<IEnumerable<object>> input) { this.Input = input; }

    /// <summary>This endpoint can receive input in multiple ways.</summary>
    public string InputLocation { get; } = "supplied";

    /// <summary>Supplying the data directly gives you the option to dynamically create email lists on the fly rather than having to write to a file.
    /// Column headers will be omitted.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    public IEnumerable<IEnumerable<object>> Input { get; }
}

/// <summary>Records have an email address and any other properties you want.</summary>
public interface ICreatRequestInputRecord { 
    string Email { get; }
}

public class JobCreateRemoteUrlRequestModel : JobCreateRequestModel
{
    public JobCreateRemoteUrlRequestModel(string input) { this.Input = input; }

    /// <summary>This endpoint can receive input in multiple ways.</summary>
    public string InputLocation { get; } = "remote_url";

    /// <summary>Using a remote URL allows you to host the file and provide us with a direct link to it. 
    /// The file should be a list of emails separated by line breaks or a standard CSV file. 
    /// We support most common file transfer protocols and their authentication mechanisms. 
    /// When using a URL that requires authentication be sure to pass the username and password in the URI string.</summary>
    /// Example CSV: <code>
    /// id,email,name
    /// "12345","support@neverbounce.com","Fred McValid"
    /// "12346","invalid@neverbounce.com","Bob McInvalid"
    /// </code>
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
