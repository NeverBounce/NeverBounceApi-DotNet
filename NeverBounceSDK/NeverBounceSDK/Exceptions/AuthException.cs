using System;
namespace NeverBounce.Exceptions
{
    [Serializable()]
    public class AuthException : Exception
    {
		public AuthException() : base() { }
		public AuthException(string message) : base(message) { }
		public AuthException(string message, Exception inner) : base(message, inner) { }

		// A constructor is needed for serialization when an
		// exception propagates from a remoting server to the client. 
		protected AuthException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
		{ }
    }
}