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
using System.Linq;
using System.Text.RegularExpressions;
using NvdaTestingDriver.Extensions;

namespace NvdaTestingDriver
{
	/// <summary>
	/// Class that includes functions to help with the creation of tests with NvdaTestingDriver.
	/// </summary>
	public static class NvdaTestHelper
	{
		private static readonly Regex _lineBreakssRegex = new Regex(@"[\r\n]");

		private static readonly Regex _spacesAndTabsRegex = new Regex(@"[\s\t]+");

		/// <summary>
		/// Checks whether the string <paramref name="text" /> contains the string <paramref name="expectedText" />.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="expectedText">The expected text.</param>
		/// <returns>true if <paramref name="text"/> contains <paramref name="expectedText"/>, false, otherwise</returns>
		public static bool TextContains(string text, string expectedText)
		{
			text = NormalizeText(text);
			expectedText = NormalizeText(expectedText);

			return text.Contains(expectedText, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Normalizes the text, removing spaces, tabs, line breaks and as long as
		/// the NvdaRemote addon changes iphens by spaces sometimes, it also replaces iphens by spaces.
		/// </summary>
		/// <param name="text">The text to normalize.</param>
		/// <returns>The normalized text</returns>
		private static string NormalizeText(string text)
		{
			text = _lineBreakssRegex.Replace(text, " ");
			text = _spacesAndTabsRegex.Replace(text, " ");
			text = text.Trim();

			// There is a bug in NVDA remote, which replace hyphens by spaces in spoken message in some situations.
			// As long as it is not fixed, it is necessary to remove the
			// hyphens from the texts to avoid inconsistencies between the message returned
			// in the voice viewer, and the message returned by NVDA to the driver.
			text = text.Replace("-", " ");
			return text;
		}
	}
}
