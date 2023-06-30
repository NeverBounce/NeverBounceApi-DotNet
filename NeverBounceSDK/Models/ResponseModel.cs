namespace NeverBounce.Models;

/// <summary>Generic response properties</summary>
public class ResponseModel
{
    /// <summary>All 2xx level responses will contain a status property.
    /// This property will indicate whether the requested operation was successfully completed or if an error was encountered.
    /// 
    /// <para>When an error does occur a message property will be included with a detailed message about why it failed.
    /// These error messages will be returned with a 200 level status code.</para></summary>
    public ResponseStatus Status { get; set; } = ResponseStatus.Success;

    /// <summary>When an error does occur a message property will be included with a detailed message about why it faied.</summary>
    public string? Message { get; set; } = null;

    public int ExecutionTime { get; set; }
}
