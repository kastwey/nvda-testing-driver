using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessibleDemo.Core.Tests.Exceptions
{

	/// <summary>
	/// The exception that is thrown when the result of a function is not the expected result.
	/// </summary>
	/// <seealso cref="System.Exception" />
	[Serializable]
	public class UnexpectedResultException : Exception
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="UnexpectedResultException"/> class.
		/// </summary>
		public UnexpectedResultException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnexpectedResultException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public UnexpectedResultException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnexpectedResultException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner.</param>
		public UnexpectedResultException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnexpectedResultException"/> class.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		protected UnexpectedResultException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}
	}
}
