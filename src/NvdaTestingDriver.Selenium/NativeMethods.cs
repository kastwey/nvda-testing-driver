// Copyright (C) 2019 Juan José Montiel
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

namespace NvdaTestingDriver.Selenium
{
	internal static class NativeMethods
	{
		/// <summary>
		/// Sets foreground window by calling window API.
		/// </summary>
		/// <param name="hWnd">The h WND.</param>
		/// <returns>true if function is able to bring the window in foreground, otherwise, false</returns>
		[DllImport("user32.dll")]
		internal static extern bool SetForegroundWindow(IntPtr hWnd);
	}
}