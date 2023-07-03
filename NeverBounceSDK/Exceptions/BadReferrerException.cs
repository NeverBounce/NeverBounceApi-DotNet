namespace NeverBounce.Exceptions;
using System.Runtime.Serialization;

[Serializable]
public class BadReferrerException : Exception
{
    public BadReferrerException()
    {
    }

    public BadReferrerException(string message) : base(message)
    {
    }

    public BadReferrerException(string message, Exception inner) : base(message, inner)
    {
    }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected BadReferrerException(SerializationInfo info,
        StreamingContext context)
    {
    }
}
