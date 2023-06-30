namespace NeverBounce.Models;

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
