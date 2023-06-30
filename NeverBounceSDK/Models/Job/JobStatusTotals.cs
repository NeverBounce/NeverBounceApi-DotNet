namespace NeverBounce.Models;

/// <summary>See https://developers.neverbounce.com/reference/jobs-status#totals</summary>
public class JobStatusTotals
{
    /// <summary>The number of rows we found in your file. 
    /// This may include whitespace, empty rows, and other rows that don't contain syntactically correct emails.</summary>
    public int? Records { get; set; }

    /// <summary>The number of rows we found syntactically correct emails in. 
    /// This is number of credits processing this job will consume.</summary>
    public int? Billable { get; set; }

    /// <summary>The total number of emails that have been processed so far.</summary>
    public int? Processed { get; set; }

    /// <summary>The number of rows that contain duplicated emails.
    /// This is not the number of unique duplicates but the total of every instance.</summary>
    public int? Duplicates { get; set; }

    /// <summary>The number of records that do not contain data that looks like an email.
    /// This may include whitespace and empty rows as well as bad data.</summary>
    public int? BadSyntax { get; set; }



    public int? Valid { get; set; }

    public int? Invalid { get; set; }

    public int? Catchall { get; set; }

    public int? Disposable { get; set; }

    public int? Unknown { get; set; }
}
