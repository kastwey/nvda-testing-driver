using LoggingAdvanced.Console;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace NvdaTestingDriver.ConsoleTests
{
	class Program
	{
		static async Task Main(string[] args)
		{
			using (var nvdaDriver = new NvdaDriver(opt =>
			{
				opt.LoggerFactory = new LoggerFactory()
								.AddConsoleAdvanced(LogLevel.Debug)
								.AddDebug(LogLevel.Debug);
			}))
			{
				try
				{
					await nvdaDriver.ConnectAsync();
					Console.WriteLine(await nvdaDriver.SendCommandAndGetSpokenTextAsync(NvdaTestingDriver.Commands.NvdaCommands.NavigatingSystemFocusCommands.ReportTitle));
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception! " + ex.Message);
				}
				Console.WriteLine("Ya ha escrito todo.");
			}
			Console.ReadLine();
		}
	}
}
