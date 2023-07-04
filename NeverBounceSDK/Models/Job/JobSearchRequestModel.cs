﻿namespace NeverBounce.Models;

/// <summary>Request object to serialise and send to the /job/search endpoint</summary>
public class JobSearchRequestModel
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
