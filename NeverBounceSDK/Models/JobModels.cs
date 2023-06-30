namespace NeverBounce.Models;

public class JobSearchRequestModel : RequestModel
{
    /// <summary>Filter jobs based on its ID</summary>
    public int? JobID { get; set; }

    /// <summary>Filter jobs based on the filename (exact match)</summary>
    public string? Filename { get; set; }

    /// <summary>Filter jobs by the job status</summary>
    public JobStatus? JobStatus { get; set; }

    /// <summary>The page to grab the jobs from</summary>
    public int Page { get; set; } = 1;

    /// <summary>The number of jobs to display</summary>
    public int ItemsPerPage { get; set; } = 10;
}

public class JobSearchResponseModel : ResponseModel
{
    public int TotalResults { get; set; }

    public int TotalPages { get; set; }

    public JobSearchQuery Query { get; set; }

    public IEnumerable<JobStatusResult> Results { get; set; }
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
    public string? CallbackUrl { get; set; }

    /// <summary>An optional array of headers that should be included when sending events to the callback_url</summary>
    public Dictionary<string, string>? CallbackHeaders { get; set; }

    /// <summary>Miscellanious request meta data</summary>
    public RequestMetaDataModel RequestMetaData { get; set; } = new RequestMetaDataModel();
}

public class JobCreateSuppliedDataRequestModel : JobCreateRequestModel
{
    public JobCreateSuppliedDataRequestModel(IEnumerable<ICreateRequestInputRecord> input) { this.Input = input; }

    /// <summary>This endpoint can receive input in multiple ways.</summary>
    public string InputLocation { get; } = "supplied";

    /// <summary>Supplying the data directly gives you the option to dynamically create email lists on the fly rather than having to write to a file.
    /// The key names will be used for the column headers.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    public IEnumerable<ICreateRequestInputRecord> Input { get; }
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
public interface ICreateRequestInputRecord
{
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

public class JobParseRequestModel : JobStatusRequestModel
{
    public JobParseRequestModel(int jobID) : base(jobID) { }

    /// <summary>Should the job start processing immediately after it's parsed? (default: false)</summary>
    public bool AutoStart { get; set; } = false;
}

public class JobQueueResponseModel : ResponseModel
{
    public string QueueID { get; set; }
}

public class JobStartRequestModel : JobStatusRequestModel
{
    public JobStartRequestModel(int jobID): base(jobID) { }

    /// <summary>Should this job be run as a sample? (default: false)</summary>
    public bool RunSample { get; set; } = false;
}

public class JobStatusRequestModel : RequestModel
{
    public JobStatusRequestModel(int jobID) { this.JobID = jobID; }

    /// <summary>The ID of the job to action (required)</summary>
    public int JobID { get; }
}

public enum JobStatus
{
    /// <summary>The job has fallen into our Q/A review and requires action on our end. 
    /// <para>Read more: https://neverbounce.com/help/clean/what-is-my-list-under-a-qa-review</para></summary>
    UnderReview,

    /// <summary>The job has been queued, this is because we're either too busy or you have too many active jobs.</summary>
    Queued,

    /// <summary>The job failed, typically this is due to poorly formatted data.</summary>
    Failed,

    /// <summary>The job has completed verification and the results are ready for download.</summary>
    Complete,

    /// <summary>The job is currently either running a sample or full verification.</summary>
    Running,

    /// <summary>The job has been received and we are parsing the data for emails and duplicate records.</summary>
    Parsing,

    /// <summary>The job has not yet run and is waiting to be started.</summary>
    Waiting,

    /// <summary>The job has completed analysis and is waiting for further action.</summary>
    WaitingAnalyzed,

    /// <summary>The job is currently being uploaded to the system.</summary>
    Uploading,
}


public enum FailReason
{ 
    /// <summary>We were un-able to detect this file's encoding type. 
    /// Please re-save your file choosing UTF-8 encoding for best results.</summary>
    UnknownFileEncoding,

    /// <summary>We were unable to parse the file. 
    /// Please add some data to the file and try again.</summary>
    EmptyFile,

    /// <summary>We were unable to parse the file. 
    /// Please split your file in smaller chunks and try again.</summary>
    FileTooLarge,

    /// <summary>This file appears to be corrupt, please try re-saving the original file or contact support for further assistance.</summary>
    FileCorrupt,
}

public class JobStatusResponseModel : ResponseModel
{
    public int ID { get; set; }

    /// <summary>Job status will indicate what stage the job is currently in. 
    /// This will be the primary property you'll want to check to determine what can be done with the job.</summary>
    public JobStatus JobStatus { get; set; }

    /// <summary>This will be what's displayed in the dashboard when viewing this job</summary>
    public string Filename { get; set; }

    public string CreatedAt { get; set; }
    public object StartedAt { get; set; }
    public object FinishedAt { get; set; }

    /// <summary>There are several items in the total object that give you an overview of verification results. 
    /// These numbers are updated periodically during the verification process.</summary>
    public JobsTotals Total { get; set; }

    /// <summary>This property indicates the bounce rate we estimate the list to bounce at if sent to in its entirety. 
    /// The value of this property will be a float between 0.0 and 100.0.</summary>
    public float BounceEstimate { get; set; }

    /// <summary>This property indicates the overall progress of this job's verification. 
    /// The value of this property will be a float between 0.0 and 100.0.</summary>
    public float PercentComplete { get; set; }

    /// <summary>If the job has failed this will hold the reason, if known.</summary>
    public FailReason? FailureReason { get; set; }
}

public class JobResultsRequestModel : JobStatusRequestModel
{
    public JobResultsRequestModel(int jobID) : base(jobID) { }

    /// <summary>The page to return the results from</summary>
    public int Page { get; set; } = 1;

    /// <summary>The number of results to be returned, between 1 and 1000</summary>
    public int ItemsPerPage { get; set; } = 10;
}

public class JobResultsResponseModel : ResponseModel
{
    public int TotalResults { get; set; }

    public int TotalPages { get; set; }

    public JobsResultsQuery Query { get; set; }

    public IEnumerable<EmailCheckResult> Results { get; set; }
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


public class EmailCheckResult
{
    /// <summary>The data object will contain the original data submitted for this row. 
    /// If the source data was submitted via the API the data will have the same keys as when it was originally submitted. 
    /// If submitted as a CSV in the dashboard the data will use the header row to determine the keys for the data when available.</summary>
    public Dictionary<string, object> Data { get; set; }

    /// <summary>The verification object contains the verification results for the email being verified. </summary>
    public SingleResponseModel Verification { get; set; }
}

public class JobDownloadRequestModel : JobStatusRequestModel
{
    public JobDownloadRequestModel(int jobID) : base(jobID) { }

    /// <summary>Includes or excludes valid emails</summary>
    public bool Valids { get; set; } = true;

    /// <summary>Includes or excludes invalid emails</summary>
    public bool Invalids { get; set; } = true;

    /// <summary>Includes or excludes catchall (accept all / unverifiable) emails</summary>
    public bool Catchalls { get; set; } = true;

    /// <summary>Includes or excludes unknown emails</summary>
    public bool Unknowns { get; set; } = true;

    /// <summary>Includes or excludes disposable emails</summary>
    public bool Disposables { get; set; } = true;

    /// <summary>If true then all instances of duplicated items will appear.</summary>
    public bool IncludeDuplicates { get; set; } = false;

    /// <summary>If set this property overrides other segmentation options and the download will only return the duplicated items.</summary>
    public bool OnlyDuplicates { get; set; } = false;

    /// <summary>If set this property overrides other segmentation options and the download will only return bad syntax records.</summary>
    public bool OnlyBadSyntax { get; set; } = false;
}

