using System;
namespace NeverBounce.Exceptions
{
    [Serializable()]
    public class BadReferrerException : Exception
    {
		public BadReferrerException() : base() { }
		public BadReferrerException(string message) : base(message) { }
		public BadReferrerException(string message, Exception inner) : base(message, inner) { }

		// A constructor is needed for serialization when an
		// exception propagates from a remoting server to the client. 
		protected BadReferrerException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
		{ }
    }
}