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
using OpenQA.Selenium;

namespace NvdaTestingDriver.Selenium.Extensions
{
	/// <summary>
	/// Class to extend IWebDriver with new functionality
	/// </summary>
	public static class SeleniumIWebDriverExtensions
	{
		/// <summary>
		/// Focuses into the specified element.
		/// </summary>
		/// <param name="webDriver">The web driver.</param>
		/// <param name="element">The element.</param>
		/// <exception cref="ArgumentNullException">element</exception>
		public static void Focus(this IWebDriver webDriver, IWebElement element)
		{
			if (webDriver is null)
			{
				throw new ArgumentNullException(nameof(webDriver));
			}

			var javascriptExecutor = (IJavaScriptExecutor)webDriver;
			if (element == null)
			{
				throw new ArgumentNullException(nameof(element));
			}

			string javascriptInstructions = string.Empty;

			bool hasTabIndexAttribute = !string.IsNullOrWhiteSpace(element.GetAttribute("tabindex"));
			if (!hasTabIndexAttribute)
			{
				javascriptInstructions = "arguments[0].setAttribute(\"tabindex\", \"-1\");\n";
			}

			javascriptInstructions += "arguments[0].focus();\n";
			javascriptExecutor.ExecuteScript(javascriptInstructions, element);
		}

		public static void FocusOnWindow(this IWebDriver webDriver)
		{
			((IJavaScriptExecutor)webDriver).ExecuteScript("window.focus();");
		}
	}
}