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

    /// <summary>
    ///     This method calls the search job end points.
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-search"
    /// </summary>
    /// <param name="model">JobSearchRequestModel</param>
    /// <returns>JobSearchResponseModel</returns>
    public async Task<JobSearchResponseModel?> Search(JobSearchRequestModel model) => 
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
    public async Task<int?> Create(JobCreateSuppliedDataRequestModel model) => 
        (await this.client.RequestPost<JobCreateResponseModel>( "/jobs/create", model))?.JobID;

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.
    /// <para>The API enforces a max request size of 25 Megabytes. 
    /// If you surpass this limit you'll receive a 413 Entity Too Large error from the server. 
    /// For payloads that exceed 25 Megabytes we suggest using the remote_url method or removing any ancillary data sent with the emails.</para></summary>
    /// <param name="model">A model that includes array data to check</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int?> Create(JobCreateSuppliedArrayRequestModel model) =>
        (await this.client.RequestPost<JobCreateResponseModel>("/jobs/create", model))?.JobID;

    /// <summary>The jobs create endpoint allows you create verify multiple emails together, the same way you would verify lists in the dashboard. 
    /// This endpoint will create a job and process the emails in the list (if auto_start is enabled) asynchronously. 
    /// Verification results are not returned in the response.</summary>
    /// <param name="model">A model that includes a link to a CSV to check</param>
    /// <returns>ID of the job created. Use this ID to check progress or make changes.</returns>
    public async Task<int?> Create(JobCreateRemoteUrlRequestModel model) => 
        (await this.client.RequestPost<JobCreateResponseModel>( "/jobs/create", model))?.JobID;

    #endregion

    /// <summary>
    ///     This method calls the parse job end point
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-parse"
    /// </summary>
    /// <param name="model">JobParseRequestModel</param>
    /// <returns>JobParseResponseModel</returns>
    public async Task<JobParseResponseModel?> Parse(JobParseRequestModel model) => 
        await this.client.RequestPost<JobParseResponseModel>( "/jobs/parse", model);

    /// <summary>
    ///     This method calls the start job end point
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-start"
    /// </summary>
    /// <param name="model">JobStartRequestModel</param>
    /// <returns>JobStartResponseModel</returns>
    public async Task<JobStartResponseModel?> Start(JobStartRequestModel model) => 
        await this.client.RequestPost<JobStartResponseModel>( "/jobs/start", model);

    /// <summary>
    ///     This method calls the job status endpoint
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-status"
    /// </summary>
    /// <param name="model">JobStatusRequestModel</param>
    /// <returns>JobStatusResponseModel</returns>
    public async Task<JobStatusResponseModel?> Status(JobStatusRequestModel model) =>
        await this.client.RequestGet<JobStatusResponseModel>( "/jobs/status", model);

    /// <summary>
    ///     This method calls the job results endpoint
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-results"
    /// </summary>
    /// <param name="model">JobResultsRequestModel</param>
    /// <returns>JobResultsResponseModel</returns>
    public async Task<JobResultsResponseModel?> Results(JobResultsRequestModel model) => 
        await this.client.RequestGet<JobResultsResponseModel>( "/jobs/results", model);

    /// <summary>
    ///     This method calls the job download endpoint; this endpoint returns the
    ///     CSV data for the job
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-download"
    /// </summary>
    /// <param name="model">JobDownloadRequestModel</param>
    /// <returns>string</returns>
    public async Task<string?> Download(JobDownloadRequestModel model) =>
        // expect "application/octet-stream"
        await client.RequestGetBody( "/jobs/download", model);
    
    /// <summary>
    ///     This method calls the job delete endpoint
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-delete"
    /// </summary>
    /// <param name="model">JobDeleteRequestModel</param>
    /// <returns>JobResultsResponseModel</returns>
    public async Task<JobDeleteResponseModel?> Delete(JobDeleteRequestModel model) => 
        await this.client.RequestGet<JobDeleteResponseModel>( "/jobs/delete", model);
}
