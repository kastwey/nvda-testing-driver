// Copyright (C) 2019 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NvdaTestingDriver.MSTest
{
	/// <summary>
	/// Class to use assertions for testing with NVDA, bassed on MSTest AssertFailedException
	/// </summary>
	/// <seealso cref="NvdaTestingDriver.GenericNvdaAssert{Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException}" />
	public static class NvdaAssert
	{
		/// <summary>
		/// Checks whether the string <paramref name="text" /> contains the string <paramref name="expectedText" />.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="expectedText">The expected text.</param>
		/// <exception cref="AssertFailedException">Expected: \"{expectedText}\". NVDA said: \"{text}</exception>
		public static void TextContains(string text, string expectedText)
		{
			if (!NvdaTestHelper.TextContains(text, expectedText))
			{
				throw new AssertFailedException($"Expected: \"{expectedText}\". NVDA said: \"{text}\"");
			}
		}
	}
}