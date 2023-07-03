namespace NeverBounce.Exceptions;
using System.Runtime.Serialization;


[Serializable]
public class AuthException : Exception
{
    public AuthException()
    {
    }

    public AuthException(string message) : base(message)
    {
    }

    public AuthException(string message, Exception inner) : base(message, inner)
    {
    }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected AuthException(SerializationInfo info,
        StreamingContext context)
    {
    }
}