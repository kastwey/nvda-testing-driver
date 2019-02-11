// Copyright (C) 2019 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System;
using System.Runtime.Serialization;

namespace NvdaTestingDriver.Exceptions
{
	/// <summary>
	/// represents the error that occurs when trying to execute an NVDA command without having previously connected the driver, or if the driver was disconnected for any reason.
	/// </summary>
	/// <seealso cref="NvdaTestingDriver.Exceptions.NvdaException" />
	[Serializable]
	public class NotConnectedException : NvdaException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NotConnectedException"/> class.
		/// </summary>
		public NotConnectedException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotConnectedException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public NotConnectedException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotConnectedException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public NotConnectedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotConnectedException" /> class.
		/// </summary>
		/// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
		protected NotConnectedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}