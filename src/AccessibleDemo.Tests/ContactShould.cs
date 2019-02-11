using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NvdaTestingDriver.Commands.NvdaCommands;
using NvdaTestingDriver.MSTest;
using NvdaTestingDriver.Selenium.Extensions;

namespace AccessibleDemo.Tests
{
	[TestClass]
	public class ContactShould
	{

		[TestMethod]
		public async Task CheckContactHeadersReadCollapsiblePannels()
		{
			// Arrange:
			TestHelper.WebDriverWrapper.SetBrowserWindowForeground();
			TestHelper.WebDriver.Navigate().GoToUrl("https://localhost:5001/home/contact");
			TestHelper.WebDriver.FocusOnWindow();

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
			TestHelper.WebDriver.Navigate().GoToUrl("https://localhost:5001/home/contact");
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
