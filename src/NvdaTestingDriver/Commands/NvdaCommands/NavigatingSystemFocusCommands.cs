// Copyright (C) 2019 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

namespace NvdaTestingDriver.Commands.NvdaCommands
{
	/// <summary>
	/// Group all NVDA commands under navigating with system focus category
	/// </summary>
	public static class NavigatingSystemFocusCommands
	{
		/// <summary>
		/// Gets the command to announces the current object or control that has the System focus.
		/// </summary>
		/// <value>
		/// The report current focus command.
		/// </value>
		public static NvdaCommand ReportCurrentFocus => new NvdaCommand(new KeyCombination { Key.Nvda, Key.Tab });

		/// <summary>
		/// Gets the command to reports the title of the currently active window.
		/// </summary>
		/// <value>
		/// The report title command.
		/// </value>
		public static NvdaCommand ReportTitle => new NvdaCommand(new KeyCombination { Key.Nvda, Key.T }, nameof(ReportTitle));

		/// <summary>
		/// Gets the command to reads all the controls in the currently active window (useful for dialogs).
		/// </summary>
		/// <value>
		/// The read active window command.
		/// </value>
		public static NvdaCommand ReadActiveWindow => new NvdaCommand(new KeyCombination { Key.Nvda, Key.B });

		/// <summary>
		/// Gets the command to reports the Status Bar if NVDA finds one. It also moves the navigator object to this location.
		/// </summary>
		/// <value>
		/// The report status bar command.
		/// </value>
		public static NvdaCommand ReportStatusBar => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.End },
			new KeyCombination { Key.Nvda, Key.Shift, Key.End });
	}
}