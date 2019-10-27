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

namespace NvdaTestingDriver.Settings
{
	/// <summary>
	/// The keyboard layout applied to NVDA
	/// </summary>
	public enum KeyboardLayout
	{
		/// <summary>
		/// The desktop scheme
		/// </summary>
		Desktop,

		/// <summary>
		/// The laptop scheme
		/// </summary>
		Laptop,
	}

	/// <summary>
	/// The NVDA progress bar output modes
	/// </summary>
	[Flags]
	public enum ProgressBarOutputModes
	{
		/// <summary>
		/// Only anounce progress with beeps
		/// </summary>
		Beep = 1,

		/// <summary>
		/// Speaks the progress percentaje
		/// </summary>
		Speak = 2,
	}

	/// <summary>
	/// The NVDA punctuation level
	/// </summary>
	public enum PunctuationLevel
	{
		/// <summary>
		///  No punctuation
		/// </summary>
		None,

		/// <summary>
		///  Some punctuation
		/// </summary>
		Some,

		/// <summary>
		/// Most punctuation
		/// </summary>
		Most,

		/// <summary>
		///  All punctuation
		/// </summary>
		All,
	}
}