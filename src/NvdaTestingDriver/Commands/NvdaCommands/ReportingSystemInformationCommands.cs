// Copyright (C) 2019 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System.Collections.Generic;

namespace NvdaTestingDriver.Commands.NvdaCommands
{
	/// <summary>
	/// Class to group all commands about Reporting System Information category.
	/// </summary>
	public static class ReportingSystemInformationCommands
	{
		/// <summary>
		/// Gets the command to report the system time.
		/// </summary>
		/// <value>
		/// The report time command.
		/// </value>
		public static NvdaCommand ReportTime => new NvdaCommand(new KeyCombination { Key.Nvda, Key.F12 });

		/// <summary>
		/// Gets the command to report the system date
		/// </summary>
		/// <value>
		/// The report date command.
		/// </value>
		public static NvdaCommand ReportDate => new NvdaCommand(
			new List<KeyCombination>
			{
			new KeyCombination { Key.Nvda, Key.F12 },
			new KeyCombination { Key.Nvda, Key.F12 },
			});

		/// <summary>
		/// Gets the command to reports the Text in the clipboard if there is any.
		/// </summary>
		/// <value>
		/// The report clipboard text command.
		/// </value>
		public static NvdaCommand ReportClipboardText => new NvdaCommand(new KeyCombination { Key.WindowsKey, Key.C });
	}
}
