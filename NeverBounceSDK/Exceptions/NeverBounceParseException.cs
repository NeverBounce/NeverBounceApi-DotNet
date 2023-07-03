namespace NeverBounce.Exceptions;
using System.Runtime.Serialization;

/// <summary>Thrown when there is an error parsing the response from the server</summary>
[Serializable]
public class NeverBounceParseException : Exception
{
    public NeverBounceParseException(string message) : base(message)
    {
    }

    public NeverBounceParseException(string message, Exception inner) : base(message, inner)
    {
    }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected NeverBounceParseException(SerializationInfo info,
        StreamingContext context)
    {
    }
}
