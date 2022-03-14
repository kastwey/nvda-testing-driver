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
using System.Runtime.InteropServices;
using NvdaTestingDriver.Selenium.Exceptions;

namespace NvdaTestingDriver.Selenium
{
	internal static class NativeMethods
	{
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// When you don't want the ProcessId, use this overload and pass 
		// IntPtr.Zero for the second parameter
		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		/// The GetForegroundWindow function returns a handle to the 
		/// foreground window.
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(HandleRef hWnd);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

		/// <summary>
		/// Forces a windows to be bringt to foreground.
		/// </summary>
		/// <param name="hWnd">The h WND.</param>
		/// <returns></returns>
		public static bool ForceForegroundWindow(IntPtr hWnd)
		{

			try
			{
				uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
				uint appThread = GetCurrentThreadId();
				const uint SW_SHOW = 5;
				if (foreThread != appThread)
				{
					EnsureFuncReturnsTrue(() => AttachThreadInput(foreThread, appThread, true));
					EnsureFuncReturnsTrue(() => BringWindowToTop(hWnd));
					EnsureFuncReturnsTrue(() => ShowWindow(hWnd, SW_SHOW));
					EnsureFuncReturnsTrue(() => AttachThreadInput(foreThread, appThread, false));
				}
				else
				{
					EnsureFuncReturnsTrue(() => BringWindowToTop(hWnd));
					EnsureFuncReturnsTrue(() => ShowWindow(hWnd, SW_SHOW));
				}

				return true;
			}
			catch (UnexpectedResultException)
			{
				return false;
			}
		}

		private static void EnsureFuncReturnsTrue(Func<bool> action)
		{
			var result = action();
			if (!result)
			{
				// throw new UnexpectedResultException("The function returned false. Expected: true.");
			}
		}
	}
}
