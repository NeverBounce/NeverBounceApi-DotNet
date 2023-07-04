namespace NeverBounce.Exceptions;
using NeverBounce.Models;
using System.Runtime.Serialization;

/// <summary>Thrown when the NeverBounce service returns an error message.</summary>
[Serializable]
public class NeverBounceServiceException : Exception
{
    public NeverBounceServiceException(ResponseStatus reason) { this.Reason = reason; }

    public NeverBounceServiceException(ResponseStatus reason, string message) : base(message)
    {
        this.Reason = reason;
    }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected NeverBounceServiceException(SerializationInfo info,
        StreamingContext context)
    {
    }

    public ResponseStatus Reason { get; set; } = ResponseStatus.GeneralFailure;
}
