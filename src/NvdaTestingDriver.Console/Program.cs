using System;
using System.Threading.Tasks;
using NvdaTestingDriver.Commands.NvdaCommands;

namespace NvdaTestingDriver.Console
{
	public static class Program
	{
		static async Task Main(string[] args)
		{
			using (var nvdaDriver = new NvdaDriver(options =>
			{
				options.GeneralSettings.PlayStartAndExitSounds = false;
			}))
			{
				await nvdaDriver.ConnectAsync();
				try
				{
					var message = await nvdaDriver.SendCommandAndGetSpokenTextAsync(NavigatingSystemFocusCommands.ReportTitle);
					System.Console.WriteLine("Message: " + message);
				}	
				catch (Exception ex)
				{
					System.Console.WriteLine(ex.Message);
				}
			}
			System.Console.ReadLine();
		} 
	}
}
