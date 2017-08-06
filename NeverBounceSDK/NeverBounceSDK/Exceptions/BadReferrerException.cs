using System;
namespace NeverBounce.Exceptions
{
    [Serializable()]
    public class GeneralException : Exception
    {
		public GeneralException() : base() { }
		public GeneralException(string message) : base(message) { }
		public GeneralException(string message, Exception inner) : base(message, inner) { }

		// A constructor is needed for serialization when an
		// exception propagates from a remoting server to the client. 
		protected GeneralException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
		{ }
    }
}