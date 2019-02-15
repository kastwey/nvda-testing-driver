using Microsoft.VisualStudio.TestTools.UnitTesting;
using NvdaTestingDriver;
using NvdaTestingDriver.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AccessibleDemo.Core.Tests
{
	[TestClass]
	public static class TestHelper
	{

		internal static WebDriverWrapper WebDriverWrapper = new WebDriverWrapper();

		internal static IWebDriver WebDriver { get; private set; }


		internal static NvdaDriver NvdaDriver;

		private static Process _webProcess;

		[AssemblyInitialize]
		public static async Task Initialize(TestContext context)
		{
			UpWebApp();
			UpWebDriver();
			await ConnectNvdaDriverAsync();
		}


		private static void UpWebApp()
		{
			try
			{
				var workingDir = Path.Combine(Path.GetDirectoryName(typeof(TestHelper).Assembly.Location), $@"..\..\..\..\AccessibleDemo\");
				if (!Directory.Exists(workingDir))
				{
					throw new DirectoryNotFoundException(workingDir);
				}

				var processParameters = new ProcessStartInfo
				{
					FileName = "dotnet.exe",
					WorkingDirectory = workingDir,
					Arguments = "run",
					RedirectStandardError = true,
					RedirectStandardOutput = true
				};
				_webProcess = new Process();
				_webProcess.OutputDataReceived += (sender, data) => { Console.WriteLine(data.Data); Debug.WriteLine(data.Data); };
				_webProcess.ErrorDataReceived += (sender, data) => { Console.WriteLine(data.Data); Debug.WriteLine(data.Data); };
				_webProcess.StartInfo = processParameters;
				_webProcess.EnableRaisingEvents = true;
				_webProcess.Start();
				_webProcess.BeginErrorReadLine();
				_webProcess.BeginOutputReadLine();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while starting webapp: {ex.Message}");
				throw ex;
			}
		}

		private static void UpWebDriver()
		{
			try
			{
				WebDriver = WebDriverWrapper.UpWebDriver(() =>
				{
					var op = new ChromeOptions
					{
						AcceptInsecureCertificates = true
					};
					var webDriver = new ChromeDriver(Environment.CurrentDirectory, op);
					webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMinutes(3);
					webDriver.Manage().Window.Maximize();
					return webDriver;
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while starting Chrome WebDriver: {ex.Message}");
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
				});
				await NvdaDriver.ConnectAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while starting NVDA driver: {ex.Message}");
				throw ex;
			}
		}

		private static void EndProcessTree(int pid)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "taskkill",
				Arguments = $"/pid {pid} /f /t",
				CreateNoWindow = true,
				UseShellExecute = false
			}).WaitForExit();
		}



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
				EndProcessTree(_webProcess.Id);
				_webProcess.WaitForExit();
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
