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

    /// <summary>
    ///     This method calls the create job end point using supplied data for input
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-create"
    /// </summary>
    /// <param name="model">JobCreateRequestModel</param>
    /// <returns>JobCreateResponseModel</returns>
    public async Task<JobCreateResponseModel?> CreateFromSuppliedData(JobCreateSuppliedDataRequestModel model) => 
        await this.client.RequestPost<JobCreateResponseModel>( "/jobs/create", model);

    /// <summary>
    ///     This method calls the create job end point using a remote URL for the input
    ///     See: "https://developers.neverbounce.com/v4.0/reference#jobs-create"
    /// </summary>
    /// <param name="model">JobCreateRemoteUrlRequestModel</param>
    /// <returns>JobCreateResponseModel</returns>
    public async Task<JobCreateResponseModel?> CreateFromRemoteUrl(JobCreateRemoteUrlRequestModel model) => 
        await this.client.RequestPost<JobCreateResponseModel>( "/jobs/create", model);

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
