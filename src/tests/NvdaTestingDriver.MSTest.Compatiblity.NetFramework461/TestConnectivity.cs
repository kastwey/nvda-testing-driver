using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NvdaTestingDriver.Settings;

namespace NvdaTestingDriver.MSTest.Compatiblity.NetFramework461
{
	[TestClass]
	public class TestConnectivity
	{
		[TestMethod]
		public void NvdaShouldConnectAndGetSystemDate()
		{
			using (var driver = new NvdaDriver(opt =>
			{
				opt.GeneralSettings.Language = NvdaLanguage.English;
				}))
			{
				driver.ConnectAsync().Wait();
				string text = driver.SendCommandAndGetSpokenTextAsync(NvdaTestingDriver.Commands.NvdaCommands.ReportingSystemInformationCommands.ReportDate).Result;
				Assert.IsFalse(String.IsNullOrEmpty(text));
			}
		}
	}
}
