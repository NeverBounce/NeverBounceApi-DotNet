﻿namespace NeverBounce.Services;
using NeverBounce.Models;
using NeverBounce.Utilities;
using System.Threading;

public sealed class JobsService
{
    readonly INeverBounceEndpoint client;

    internal JobsService(INeverBounceEndpoint client)
    {
        this.client = client;
    }

    /// <summary>List or find current jobs</summary>
    /// <param name="model">Optional flags to search or paginate the jobs</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Collection of jobs that match the requested search flags</returns>
    public async Task<JobSearchResponseModel> Search(JobSearchRequestModel? model = null, CancellationToken? cancellationToken = null) =>
        await this.client.RequestGet<JobSearchResponseModel>("jobs/search", model, cancellationToken);

    #region create bounce-check job

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    /// <param name="model">A model that includes structured data to check</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(JobCreateSuppliedDataRequestModel model, CancellationToken? cancellationToken = null) =>
        (await this.client.RequestPost<JobCreateResponseModel>("jobs/create", model, cancellationToken)).JobID;

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
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(IEnumerable<ICreateRequestInputRecord> input, string? name = null, bool autoParse = false, bool autoStart = false, bool runSample = false, CancellationToken? cancellationToken = null) =>
        await this.Create(new JobCreateSuppliedDataRequestModel(input) { Filename = name, AutoParse = autoParse, AutoStart = autoStart, RunSample = runSample }, cancellationToken);

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    /// <param name="model">A model that includes array data to check</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(JobCreateSuppliedArrayRequestModel model, CancellationToken? cancellationToken = null) =>
        (await this.client.RequestPost<JobCreateResponseModel>("jobs/create", model, cancellationToken)).JobID;

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
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(IEnumerable<IEnumerable<object>> input, string? name = null, bool autoParse = false, bool autoStart = false, bool runSample = false, CancellationToken? cancellationToken = null) =>
        await this.Create(new JobCreateSuppliedArrayRequestModel(input) { Filename = name, AutoParse = autoParse, AutoStart = autoStart, RunSample = runSample }, cancellationToken);

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.</summary>
    /// <param name="model">A model that includes a link to a CSV to check</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(JobCreateRemoteUrlRequestModel model, CancellationToken? cancellationToken = null) =>
        (await this.client.RequestPost<JobCreateResponseModel>("jobs/create", model, cancellationToken)).JobID;

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
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int> Create(string input, string? name = null, bool autoParse = false, bool autoStart = false, bool runSample = false, CancellationToken? cancellationToken = null) =>
        await this.Create(new JobCreateRemoteUrlRequestModel(input) { Filename = name, AutoParse = autoParse, AutoStart = autoStart, RunSample = runSample }, cancellationToken);

    #endregion

    #region start or parse a job that was created with auto_start or auto_parse disabled

    /// <summary>This endpoint allows you to parse a job created with auto_parse disabled. 
    /// You cannot reparse a list once it's been parsed.</summary>
    /// <param name="model">Job ID and additional flags</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the queue entry</returns>
    public async Task<string?> Parse(JobParseRequestModel model, CancellationToken? cancellationToken = null) =>
        (await this.client.RequestPost<JobQueueResponseModel>("jobs/parse", model, cancellationToken)).QueueID;

    /// <summary>This endpoint allows you to parse a job created with auto_parse disabled. 
    /// You cannot reparse a list once it's been parsed.</summary>
    /// <param name="jobID">ID of the job to parse</param>
    /// <param name="autoStart">Should the job start processing immediately after it's parsed? (default: false)</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the queue entry</returns>
    public async Task<string?> Parse(int jobID, bool autoStart = false, CancellationToken? cancellationToken = null) =>
        await this.Parse(new JobParseRequestModel(jobID) { AutoStart = autoStart }, cancellationToken);

    /// <summary>This endpoint allows you to start a job created or parsed with auto_start disabled. 
    /// Once the list has been started the credits will be deducted and the process cannot be stopped or restarted.</summary>
    /// <param name="model">Job ID and additional flags</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the queue entry</returns>
    public async Task<string?> Start(JobStartRequestModel model, CancellationToken? cancellationToken = null) =>
        (await this.client.RequestPost<JobQueueResponseModel>("jobs/start", model, cancellationToken)).QueueID;

    /// <summary>This endpoint allows you to start a job created or parsed with auto_start disabled. 
    /// Once the list has been started the credits will be deducted and the process cannot be stopped or restarted.</summary>
    /// <param name="jobID">ID of the job to start</param>
    /// <param name="runSample">Should this job be run as a sample? (default: false)</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>ID of the queue entry</returns>
    public async Task<string?> Start(int jobID, bool runSample = false, CancellationToken? cancellationToken = null) =>
        await this.Start(new JobStartRequestModel(jobID) { RunSample = runSample }, cancellationToken);

    #endregion

    /// <summary>Get the current status of a job</summary>
    /// <param name="jobID">The ID of an existing job to check.</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Current status of the job</returns>
    public async Task<JobStatusResponseModel> Status(int jobID, CancellationToken? cancellationToken = null) =>
        await this.client.RequestGet<JobStatusResponseModel>("jobs/status", new JobRequestModel(jobID), cancellationToken);

    /// <summary>Get the results of a job</summary>
    /// <param name="model">The job ID and additional flags</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Paginated results of the job</returns>
    public async Task<JobResultsResponseModel> Results(JobResultsRequestModel model, CancellationToken? cancellationToken = null) =>
        await this.client.RequestGet<JobResultsResponseModel>("jobs/results", model, cancellationToken);

    /// <summary>Get the results of a job</summary>
    /// <param name="jobID">The job ID</param>
    /// <param name="page">1-indexed number of the page to view</param>
    /// <param name="itemsPerPage">Number of items per page, max 1000</param>
    /// <param name="cancellationToken">Optional token to cancel long requests</param>
    /// <returns>Paginated results of the job</returns>
    public async Task<JobResultsResponseModel> Results(int jobID, int page = 1, int itemsPerPage = 10, CancellationToken? cancellationToken = null) =>
        await this.Results(new JobResultsRequestModel(jobID) { Page = page, ItemsPerPage = itemsPerPage }, cancellationToken);

    /// <summary>Download the CSV data for the job</summary>
    /// <param name="model">The job ID and additional flags</param>
    /// <param name="cancellationToken">Token to cancel long downloads</param>
    /// <returns>A stream of the file contents</returns>
    public async Task<Stream> Download(JobDownloadRequestModel model, CancellationToken? cancellationToken = null)
    {
        // expect "application/octet-stream"
        var content = await this.client.RequestGetContent("jobs/download", model, cancellationToken);
        return await content.ReadAsStreamAsync(cancellationToken ?? CancellationToken.None);
    }

    /// <summary>Download the CSV data for the job</summary>
    /// <param name="jobID">The ID of the job.</param>
    /// <param name="cancellationToken">Optional token to cancel long downloads</param>
    /// <returns>A stream of the file contents</returns>
    public async Task<Stream> Download(int jobID, CancellationToken? cancellationToken = null) =>
        await this.Download(new JobDownloadRequestModel(jobID), cancellationToken);

    /// <summary>Delete a job and remove all data associated with it.
    /// <para>The job and its results cannot be recovered once the job has been deleted. 
    /// If the results are needed after a job has been deleted you will need to resubmit and reverify the data.</para></summary>
    /// <param name="jobID">The ID of an existing job to delete.</param>
    public async Task Delete(int jobID) =>
        await this.client.RequestGet<ResponseModel>("jobs/delete", new JobRequestModel(jobID));
}
