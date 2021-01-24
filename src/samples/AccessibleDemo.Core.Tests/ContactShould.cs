using Microsoft.VisualStudio.TestTools.UnitTesting;
using NvdaTestingDriver;
using NvdaTestingDriver.Commands.NvdaCommands;
using NvdaTestingDriver.MSTest;
using NvdaTestingDriver.Selenium.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessibleDemo.Core.Tests
{
	[TestClass]
	public class ContactShould
	{

		[TestMethod]
		public async Task CheckContactHeadersReadCollapsiblePannels()
		{
			// Arrange:
			TestHelper.WebDriverWrapper.SetBrowserWindowForeground();
			var url = TestHelper.SeleniumServerFactory.RootUri + "/home/contact";
			TestHelper.WebDriver.Navigate().GoToUrl(url);
			TestHelper.WebDriver.FocusOnWindow();
			_ = await TestHelper.NvdaDriver.SendKeyCombinationsAndGetSpokenTextAsync(new KeyCombination(new List<Key> { Key.Control, Key.Home }));
			// Act & asserts
			string text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextHeading3);
			NvdaAssert.TextContains(text, "Contact by phone Collapsed link heading  level 3");

			text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextHeading3);
			NvdaAssert.TextContains(text, "Contact by e-mail collapsed  link heading  level 3");

			text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextHeading3);
			NvdaAssert.TextContains(text, "Postal address collapsed  link heading  level 3");

			text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextHeading3);
			NvdaAssert.TextContains(text, "Fill the contact form collapsed  link heading  level 3");
		}

		[TestMethod]
		public async Task CheckContactFormLabelsReadFieldAsEspected()
		{
			// Arrange
			TestHelper.WebDriverWrapper.SetBrowserWindowForeground();
			TestHelper.WebDriver.Navigate().GoToUrl("http://localhost:5000/home/contact");
			TestHelper.WebDriver.FocusOnWindow();

			// Act / Assers
			string text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextFormField);
			NvdaAssert.TextContains(text, "Name:  edit");

			text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextFormField);
			NvdaAssert.TextContains(text, "E mail:  edit");

			text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(BrowseModeCommands.NextFormField);
			NvdaAssert.TextContains(text, "Submit button");
		}
	}
}
