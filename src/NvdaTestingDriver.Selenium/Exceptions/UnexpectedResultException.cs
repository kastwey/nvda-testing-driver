// Copyright (C) 2020 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System;

namespace NvdaTestingDriver.Selenium.Exceptions
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
