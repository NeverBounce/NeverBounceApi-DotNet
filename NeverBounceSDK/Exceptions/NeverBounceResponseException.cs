namespace NeverBounce.Exceptions;
using System.Net;
using System.Runtime.Serialization;

/// <summary>Thrown when the NeverBounce service returns an error HTTP status code.</summary>
[Serializable]
public class NeverBounceResponseException : Exception
{
    public NeverBounceResponseException(HttpStatusCode status, string message) : base(message)
    {
        this.Status = status;
    }

    public NeverBounceResponseException(string message, Exception inner) : base(message, inner)
    {
    }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected NeverBounceResponseException(SerializationInfo info,
        StreamingContext context)
    {
    }

    public HttpStatusCode Status { get; set; }
}
