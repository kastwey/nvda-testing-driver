using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NvdaTestingDriver;
using NvdaTestingDriver.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace RemoteWebsites.Tests
{
	[TestClass]
	public static class TestHelper
	{

		internal static WebDriverWrapper WebDriverWrapper = new WebDriverWrapper();

		internal static IWebDriver WebDriver { get; private set; }


		internal static NvdaDriver NvdaDriver;

		/// <summary>
		/// This method will be executed before starting any test
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>The task associated to this operation</returns>
		[AssemblyInitialize]
		public static async Task Initialize(TestContext context)
		{

			// Initialize the Selenium WebDriveer
			UpWebDriver();

			// Starts the NVDATestingDriver
			await ConnectNvdaDriverAsync();
		}

		private static void UpWebDriver()
		{
			try
			{

				// We started the WebDriver using the UpWebDriver method of the WebDriverWrapper class,
				// to manage the firefox window, and get to put it in the foreground when necessary.
				WebDriver = WebDriverWrapper.UpWebDriver(() =>
				{
					var op = new FirefoxOptions
					{
						AcceptInsecureCertificates = false
					};
					var webDriver = new FirefoxDriver(Environment.CurrentDirectory, op);
					webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMinutes(3);
					webDriver.Manage().Window.Maximize();
					return webDriver;
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while starting Firefox WebDriver: {ex.Message}");
				throw;
			}
		}


		/// <summary>
		/// Connects the nvda driver asynchronously.
		/// </summary>
		/// <returns></returns>
		private static async Task ConnectNvdaDriverAsync()
		{
			try
			{
				// We start the NvdaTestingDriver:
				NvdaDriver = new NvdaDriver(opt =>
				{
					opt.GeneralSettings.Language = NvdaTestingDriver.Settings.NvdaLanguage.English;
				});
				await NvdaDriver.ConnectAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while starting NVDA driver: {ex.Message}");
				throw;
			}
		}

		/// <summary>
		/// This method will be executed when all e tests are finished.
		/// </summary>
		/// <returns></returns>
		[AssemblyCleanup]
		public static async Task Cleanup()
		{
			try
			{
				WebDriver.Quit();
			}
			catch
			{
				// If the web  driver quit fails, we continue.
			}

			try
			{
				WebDriver.Dispose();
			}
			catch
			{
				// If the web  driver dispose fails, we continue.
			}

			try
			{
				await NvdaDriver.DisconnectAsync();
			}
			catch
			{
				// if disconnect fails, we continue.
			}
		}



	}
}
