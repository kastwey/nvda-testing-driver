using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NvdaTestingDriver.Commands.NvdaCommands;
using NvdaTestingDriver.MSTest;
using NvdaTestingDriver.Selenium.Extensions;
using OpenQA.Selenium;

namespace RemoteWebsites.Tests
{
	[TestClass]
	public class GithubRepoPageShould
	{

		[TestMethod]
		public async Task CheckDownloadButtonIsCollapsibleAndExpandibleAsync()
		{
			
			// Arrange:
			// We tell the WebDriverWrapper to put the Chrome window in the foreground.
			// NVDA needs the window to be in the foreground in order to interact with that window.
			TestHelper.WebDriverWrapper.SetBrowserWindowForeground();

			// Go to dotnet core github repository:
			TestHelper.WebDriver.Navigate().GoToUrl("https://github.com/dotnet/core");
// We put the focus on chrome window:
			TestHelper.WebDriver.FocusOnWindow();

			// We put the focus inside the first summary tag with btn class:
			TestHelper.WebDriver.Focus(TestHelper.WebDriver.FindElement(By.CssSelector("summary.btn")));

			// Act & asserts
			// We send the ReportCurrentFocus command to NVDA and get the text:
			string text = await TestHelper.NvdaDriver.SendCommandAndGetSpokenTextAsync(NvdaTestingDriver.Commands.NvdaCommands.NavigatingSystemFocusCommands.ReportCurrentFocus);

			// We use the NvdaAssert.TextContains method to check that the text pronounced by NVDA
			// contains the text it should say.
			// This method sanitize both the expected text and the received text, to remove spaces, line breaks and other characters that could affect the result.
			// Whenever you want to compare text with NVDA,
			// use either the TextContains method of the NvdaAsert class (NvdaTestingDriver.MSTest package),
			// which will throw an AssertFailedException if the text specified is not present in the
			// text pronounced by NVDA, or the method TextContains of the NvdaTestHelper class
			// (NvdaTestingDriver package), which will return true or false.
			NvdaAssert.TextContains(text, "master button focused collapsed sub Menu Switch branches or tags");
		}

	}
	}
