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

namespace NvdaTestingDriver.Extensions
{
	/// <summary>
	/// Class which contains extensions for strings
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Returns a value indicating whether a specified substring occurs within this string.
		/// </summary>
		/// <param name="text">The text to seek.</param>
		/// <param name="value">The value to search.</param>
		/// <param name="comparison">The comparison to apply to this method.</param>
		/// <returns>
		///   true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.
		/// </returns>
		public static bool Contains(this string text, string value, StringComparison comparison) => text.IndexOf(value, comparison) >= 0;
	}
}