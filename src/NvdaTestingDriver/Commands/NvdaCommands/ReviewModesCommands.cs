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
	/// Class to group all review modes NVDA commands.
	/// </summary>
	public static class ReviewModesCommands
	{
		/// <summary>
		/// Gets the command to switches to the next available review mode.
		/// </summary>
		/// <value>
		/// The switch to next review mode command.
		/// </value>
		public static NvdaCommand SwitchToNextReviewMode => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPad7 },
			new KeyCombination { Key.Nvda, Key.PageUp });

		/// <summary>
		/// Gets the command to switches to the previous available review mode.
		/// </summary>
		/// <value>
		/// The switch to previous review mode command.
		/// </value>
		public static NvdaCommand SwitchToPreviousReviewMode => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPad1 },
			new KeyCombination { Key.Nvda, Key.PageDown });
	}
}
