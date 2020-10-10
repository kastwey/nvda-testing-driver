// Copyright (C) 2020 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace NvdaTestingDriver.Selenium
{
	/// <summary>
	/// Class that Wraps Selenium WebDriver, and provides it with the extra functionality needed for NVDA testing.
	/// /// </summary>
	public class WebDriverWrapper
	{
		private IntPtr _browserWindowHandle = IntPtr.Zero;

		/// <summary>
		/// Gets the web driver.
		/// </summary>
		/// <value>
		/// The web driver.
		/// </value>
		public IWebDriver WebDriver { get; private set; }

		/// <summary>
		/// Initialize the web driver.
		/// </summary>
		/// <param name="webDriverFunc">The function which will return IWebDriver instance.</param>
		/// <returns>IWebDriver instance</returns>
		public IWebDriver UpWebDriver(Func<IWebDriver> webDriverFunc)
		{
			if (webDriverFunc is null)
			{
				throw new ArgumentNullException(nameof(webDriverFunc));
			}

			var processStartTime = DateTime.Now;
			WebDriver = webDriverFunc();
			var processName = GetProcesName(WebDriver);
			var browserProcesses = Process.GetProcessesByName(processName).Where(p => p.StartTime > processStartTime && !string.IsNullOrWhiteSpace(p.MainWindowTitle)).OrderBy(p => p.StartTime).ToList();
			var process = browserProcesses.First();
			if (process != null)
			{
				_browserWindowHandle = process.MainWindowHandle;
				SetBrowserWindowForeground();
			}

			return WebDriver;
		}

		/// <summary>
		/// Activates the browser window, allowing NVDA to interact with it.
		/// </summary>
		public void SetBrowserWindowForeground()
		{
			if (_browserWindowHandle != IntPtr.Zero)
			{
				NativeMethods.ForceForegroundWindow(_browserWindowHandle);
			}

			WebDriver.Manage().Window.Maximize();
			WebDriver.Manage().Window.FullScreen();
		}

		/// <summary>
		/// Gets the name of the browser process, depending on the IWebDriver object type passed as parameter.
		/// </summary>
		/// <param name="webDriver">The web driver.</param>
		/// <returns>The browser process name</returns>
		/// <exception cref="NotSupportedException">This webdrivver is not supported in this wrapper: {webDriver.GetType().Name}</exception>
		private string GetProcesName(IWebDriver webDriver)
		{
			string processName;
			if (webDriver is ChromeDriver)
			{
				processName = "chrome";
			}
			else if (webDriver is FirefoxDriver)
			{
				processName = "firefox";
			}
			else if (webDriver is InternetExplorerDriver)
			{
				processName = "iexplore";
			}
			else if (webDriver is EdgeDriver)
			{
				processName = "edge";
			}
			else
			{
				throw new NotSupportedException($"This webdrivver is not supported in this wrapper: {webDriver.GetType().Name}.");
			}

			return processName;
		}
	}
}
