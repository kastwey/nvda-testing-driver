using System;
using System.Net.Http;
using System.Threading.Tasks;

using AccessibleDemo.Core.Tests.Logging;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NvdaTestingDriver;
using NvdaTestingDriver.Selenium;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AccessibleDemo.Core.Tests
{
	[TestClass]
	public static class TestHelper
	{

		internal static WebDriverWrapper WebDriverWrapper = new WebDriverWrapper();

		internal static IWebDriver WebDriver { get; private set; }

		internal static NvdaDriver NvdaDriver;

		internal static SeleniumServerFactory<Startup> SeleniumServerFactory;

		internal static HttpClient HttpClient;

		[AssemblyInitialize]
		public static async Task InitializeAsync(TestContext context)
		{
			SeleniumServerFactory = new SeleniumServerFactory<Startup>();
			HttpClient = SeleniumServerFactory.CreateDefaultClient();

			await ConnectNvdaDriverAsync();
			UpWebDriver();
		}

		private static void UpWebDriver()
		{
			try
			{
				WebDriver = WebDriverWrapper.UpWebDriver(() =>
				{
					var op = new FirefoxOptions
					{
						AcceptInsecureCertificates = true
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
				throw ex;
			}
		}


		private static async Task ConnectNvdaDriverAsync()
		{
			try
			{
				NvdaDriver = new NvdaDriver(opt =>
				{
					opt.GeneralSettings.Language = NvdaTestingDriver.Settings.NvdaLanguage.English;
					opt.EnableLogging = true;
					opt.LoggerFactory = LoggerFactory.Create(builder =>
					{
						builder.AddFilter("NvdaTestingDriver", Microsoft.Extensions.Logging.LogLevel.Trace);
						builder.AddConsole()
						.AddDebug()
						.AddProvider(new MSTestLoggerProvider());
					});
				});
				await NvdaDriver.ConnectAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while starting NVDA driver: {ex.Message}");
				throw ex;
			}

		}

		[AssemblyCleanup]
		public static async Task CleanupAsync()
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
				WebDriver?.Dispose();
			}
			catch
			{
				// If the web  driver dispose fails, we continue.
			}
			try
			{
				SeleniumServerFactory.Dispose();
			}
			catch
			{
				// If the web process killing fails, we continue.
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
