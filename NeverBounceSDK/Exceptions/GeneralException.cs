namespace NeverBounce.Exceptions;
using System.Runtime.Serialization;

[Serializable]
public class GeneralException : Exception
{
    public GeneralException()
    {
    }

    public GeneralException(string message) : base(message)
    {
    }

    public GeneralException(string message, Exception inner) : base(message, inner)
    {
    }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected GeneralException(SerializationInfo info,
        StreamingContext context)
    {
    }
}
