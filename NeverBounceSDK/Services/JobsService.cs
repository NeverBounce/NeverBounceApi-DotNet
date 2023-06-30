namespace NeverBounce.Services;
using NeverBounce.Models;
using NeverBounce.Utilities;

public sealed class JobsService
{
    readonly INeverBounceHttpClient client;

    public JobsService(INeverBounceHttpClient client)
    {
        this.client = client;
    }

    /// <summary>List or find current jobs</summary>
    /// <param name="model">Optional flags to search or paginate the jobs</param>
    /// <returns>Collection of jobs that match the requested search flags</returns>
    public async Task<JobSearchResponseModel> Search(JobSearchRequestModel? model = null) => 
        await this.client.RequestGet<JobSearchResponseModel>( "/jobs/search", model);

    #region create bounce-check job

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    /// <param name="model">A model that includes structured data to check</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(JobCreateSuppliedDataRequestModel model) => 
        (await this.client.RequestPost<JobCreateResponseModel>( "/jobs/create", model)).JobID;

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    /// <param name="input">Structured data to process</param>
    /// <param name="name">This will be what's displayed in the dashboard when viewing this job</param>
    /// <param name="autoParse">This will enable or disable the indexing process from automatically starting as soon as you create a job. 
    /// If set to false you will need to call the /parse endpoint after the job has been created to begin indexing.</param>
    /// <param name="autoStart">Setting this to true will start the job and deduct the credits.</param>
    /// <param name="runSample">Run a sample on the list and provide you with an estimated bounce rate without cost.</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(IEnumerable<ICreateRequestInputRecord> input, string? name = null, bool autoParse = false, bool autoStart = false, bool runSample = false) =>
        await this.Create(new JobCreateSuppliedDataRequestModel(input) { Filename = name, AutoParse = autoParse, AutoStart = autoStart, RunSample = runSample });

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    /// <param name="model">A model that includes array data to check</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(JobCreateSuppliedArrayRequestModel model) =>
        (await this.client.RequestPost<JobCreateResponseModel>("/jobs/create", model)).JobID;

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    /// <param name="input">Array data to process</param>
    /// <param name="name">This will be what's displayed in the dashboard when viewing this job</param>
    /// <param name="autoParse">This will enable or disable the indexing process from automatically starting as soon as you create a job. 
    /// If set to false you will need to call the /parse endpoint after the job has been created to begin indexing.</param>
    /// <param name="autoStart">Setting this to true will start the job and deduct the credits.</param>
    /// <param name="runSample">Run a sample on the list and provide you with an estimated bounce rate without cost.</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(IEnumerable<IEnumerable<object>> input, string? name = null, bool autoParse = false, bool autoStart = false, bool runSample = false) =>
        await this.Create(new JobCreateSuppliedArrayRequestModel(input) { Filename = name, AutoParse = autoParse, AutoStart = autoStart, RunSample = runSample });

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.</summary>
    /// <param name="model">A model that includes a link to a CSV to check</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(JobCreateRemoteUrlRequestModel model) => 
        (await this.client.RequestPost<JobCreateResponseModel>( "/jobs/create", model)).JobID;

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.</summary>
    /// <param name="input">Using a remote URL allows you to host the file and provide us with a direct link to it. 
    /// The file should be a list of emails separated by line breaks or a standard CSV file. </param>
    /// <param name="name">This will be what's displayed in the dashboard when viewing this job</param>
    /// <param name="autoParse">This will enable or disable the indexing process from automatically starting as soon as you create a job. 
    /// If set to false you will need to call the /parse endpoint after the job has been created to begin indexing.</param>
    /// <param name="autoStart">Setting this to true will start the job and deduct the credits.</param>
    /// <param name="runSample">Run a sample on the list and provide you with an estimated bounce rate without cost.</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(string input, string? name = null, bool autoParse = false, bool autoStart = false, bool runSample = false) =>
        await this.Create(new JobCreateRemoteUrlRequestModel(input) { Filename = name, AutoParse = autoParse, AutoStart = autoStart, RunSample = runSample });

    #endregion

    #region start or parse a job that was created with auto_start or auto_parse disabled

    /// <summary>This endpoint allows you to parse a job created with auto_parse disabled. 
    /// You cannot reparse a list once it's been parsed.</summary>
    /// <param name="model">Job ID and additional flags</param>
    /// <returns>ID of the queue entry</returns>
    public async Task<string> Parse(JobParseRequestModel model) => 
        (await this.client.RequestPost<JobQueueResponseModel>( "/jobs/parse", model)).QueueID;

    /// <summary>This endpoint allows you to parse a job created with auto_parse disabled. 
    /// You cannot reparse a list once it's been parsed.</summary>
    /// <param name="jobID">ID of the job to parse</param>
    /// <param name="autoStart">Should the job start processing immediately after it's parsed? (default: false)</param>
    /// <returns>ID of the queue entry</returns>
    public async Task<string> Parse(int jobID, bool autoStart = false) =>
        await this.Parse(new JobParseRequestModel(jobID) { AutoStart = autoStart });

    /// <summary>This endpoint allows you to start a job created or parsed with auto_start disabled. 
    /// Once the list has been started the credits will be deducted and the process cannot be stopped or restarted.</summary>
    /// <param name="model">Job ID and additional flags</param>
    /// <returns>ID of the queue entry</returns>
    public async Task<string> Start(JobStartRequestModel model) => 
        (await this.client.RequestPost<JobQueueResponseModel>( "/jobs/start", model)).QueueID;

    /// <summary>This endpoint allows you to start a job created or parsed with auto_start disabled. 
    /// Once the list has been started the credits will be deducted and the process cannot be stopped or restarted.</summary>
    /// <param name="jobID">ID of the job to start</param>
    /// <param name="runSample">Should this job be run as a sample? (default: false)</param>
    /// <returns>ID of the queue entry</returns>
    public async Task<string> Start(int jobID, bool runSample = false) =>
        await this.Start(new JobStartRequestModel(jobID) { RunSample = runSample });

    #endregion

    /// <summary>Get the current status of a job</summary>
    /// <param name="jobID">The ID of an existing job to check.</param>
    /// <returns>Current status of the job</returns>
    public async Task<JobStatusResponseModel> Status(int jobID) =>
        await this.client.RequestGet<JobStatusResponseModel>( "/jobs/status", new JobStatusRequestModel(jobID));

    /// <summary>Get the results of a job</summary>
    /// <param name="model">The job ID and additional flags</param>
    /// <returns>JobResultsResponseModel</returns>
    public async Task<JobResultsResponseModel> Results(JobResultsRequestModel model) => 
        await this.client.RequestGet<JobResultsResponseModel>( "/jobs/results", model);

    public async Task<JobResultsResponseModel> Results(int jobID, int page = 1, int itemsPerPage = 10) =>
        await this.Results(new JobResultsRequestModel(jobID) { Page = page, ItemsPerPage = itemsPerPage });

    /// <summary>Download the CSV data for the job</summary>
    /// <param name="model">JobDownloadRequestModel</param>
    /// <returns>string</returns>
    public async Task<string?> Download(JobDownloadRequestModel model) =>
        // expect "application/octet-stream"
        await this.client.RequestGetBody( "/jobs/download", model);

    public async Task<string?> Download(int jobID) =>
        await this.Download(new JobDownloadRequestModel(jobID));

    /// <summary>Delete a job and remove all data associated with it.
    /// <para>The job and its results cannot be recovered once the job has been deleted. 
    /// If the results are needed after a job has been deleted you will need to resubmit and reverify the data.</para></summary>
    /// <param name="jobID">The ID of an existing job to delete.</param>
    public async Task Delete(int jobID) => 
        await this.client.RequestGet<ResponseModel>( "/jobs/delete", new JobStatusRequestModel(jobID));
}
