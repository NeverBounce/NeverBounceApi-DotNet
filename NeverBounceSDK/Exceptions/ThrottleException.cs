namespace NeverBounce.Exceptions;
using System.Runtime.Serialization;

[Serializable]
public class ThrottleException : Exception
{
    public ThrottleException()
    {
    }

    public ThrottleException(string message) : base(message)
    {
    }

    public ThrottleException(string message, Exception inner) : base(message, inner)
    {
    }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected ThrottleException(SerializationInfo info,
        StreamingContext context)
    {
    }
}
