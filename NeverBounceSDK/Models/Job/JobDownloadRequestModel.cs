namespace NeverBounce.Models;

public class JobDownloadRequestModel : JobRequestModel
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
