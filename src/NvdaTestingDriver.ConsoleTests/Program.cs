using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace NvdaTestingDriver.ConsoleTests
{
	static class Program
	{
		static async Task Main()
		{
			using (var nvdaDriver = new NvdaDriver(opt =>
			{
				opt.LoggerFactory = LoggerFactory.Create(builder =>
				{
					builder.AddFilter("NvdaTestingDriver", LogLevel.Trace);
					builder.AddConsole()
					.AddDebug();
				});
				opt.EnableLogging = true;
			}))
			{
				try
				{
					await nvdaDriver.ConnectAsync();
					Console.WriteLine(await nvdaDriver.SendCommandAndGetSpokenTextAsync(NvdaTestingDriver.Commands.NvdaCommands.NavigatingSystemFocusCommands.ReportTitle, TimeSpan.FromSeconds(10)));
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
