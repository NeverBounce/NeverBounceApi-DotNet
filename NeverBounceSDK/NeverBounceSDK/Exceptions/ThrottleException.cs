using System;
namespace NeverBounce.Exceptions
{
    [Serializable()]
    public class ThrottleException : Exception
    {
		public ThrottleException() : base() { }
		public ThrottleException(string message) : base(message) { }
		public ThrottleException(string message, Exception inner) : base(message, inner) { }

		// A constructor is needed for serialization when an
		// exception propagates from a remoting server to the client. 
		protected ThrottleException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
		{ }
    }
}