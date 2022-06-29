using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NvdaTestingDriver;

namespace NvdaTestingDriver.HttpApi
{
	public static class NvdaDriverHelper
	{
		private static NvdaDriver? driver;

		public static async Task<NvdaDriver> InitializeAsync()
		{
			if (driver != null)
			{
				return driver;
			}

			try
			{
				// We start the NvdaTestingDriver:
				driver = new NvdaDriver(opt =>
				{
					opt.GeneralSettings.Language = NvdaTestingDriver.Settings.NvdaLanguage.English;
				});
				await driver.ConnectAsync();

				Debug.WriteLine("NvdaDriver was initialized");

				return driver;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while starting NVDA driver: {ex.Message}");
				throw;
			}
		}

		public static async Task CleanupAsync()
		{
			if (driver == null)
			{
				Debug.WriteLine("NvdaDriver was not started. Nothing to clean");
				return;
			}

			try
			{
				await driver.DisconnectAsync();
				Debug.WriteLine("NvdaDriver disconnected. Bye");
			}
			catch
			{
				// if disconnect fails, we continue.
			}
		}
	}
}
