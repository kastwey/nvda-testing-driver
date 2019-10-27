using Microsoft.VisualStudio.TestTools.UnitTesting;
using NvdaTestingDriver;
using NvdaTestingDriver.MSTest;
using NvdaTestingDriver.Selenium.Extensions;
using OpenQA.Selenium;
using System.Threading.Tasks;

namespace AccessibleDemo.Core.Tests
{

	[TestClass]
	public class TreeViewAccessibleShould
	{

		[TestMethod]
		public async Task CheckTreeViewInteraction()
		{
			// arrange
			TestHelper.WebDriverWrapper.SetBrowserWindowForeground();
			TestHelper.WebDriver.Navigate().GoToUrl("https://localhost:5001/home/TreeViewExample");
			TestHelper.WebDriver.FocusOnWindow();
			TestHelper.WebDriver.Focus(TestHelper.WebDriver.FindElement(By.Id("lnkWcag")));

			// act & asert
			string text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.Tab);
			NvdaAssert.TextContains(text, "All WCAG 2.1 elements  tree view level 1 1. Perceivable  collapsed  1 of");

			text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.RightArrow);
			NvdaAssert.TextContains(text, "Expanded");

			text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
			NvdaAssert.TextContains(text, "level 2  1.1. Text alternatives  1 of 4");

			text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
			NvdaAssert.TextContains(text, "1.2. Time-based Media  2 of 4  level 2");

			text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
			NvdaAssert.TextContains(text, "1.3. Adaptable  3 of 4  level 2");

			text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
			NvdaAssert.TextContains(text, "1.4. Distinguishable  4 of 4  level 2");

			text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.DownArrow);
			NvdaAssert.TextContains(text, "level 1  2. Operable  collapsed  2 of 4");

			text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.RightArrow);
			NvdaAssert.TextContains(text, "Expanded");

			text = await TestHelper.NvdaDriver.SendKeysAndGetSpokenTextAsync(Key.LeftArrow);
			NvdaAssert.TextContains(text, "collapsed");

		}

	}
}
