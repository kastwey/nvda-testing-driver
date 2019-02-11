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
	/// <summary>Group all commands for navigating with system caret</summary>
	public static class NavigatingSystemCaretCommands
	{
		/// <summary>
		/// Gets the command to starts reading from the current position of the system caret, moving it along as it goes.
		/// </summary>
		/// <value>
		/// The say all command.
		/// </value>
		public static NvdaCommand SayAll => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.PageDown },
			new KeyCombination { Key.Nvda, Key.A });

		/// <summary>
		/// Gets the command to reads the line where the system caret is currently situated. Pressing twice spells the line. Pressing three times spells the line using character descriptions.
		/// </summary>
		/// <value>
		/// The read current line command.
		/// </value>
		public static NvdaCommand ReadCurrentLine => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.UpArrow },
			new KeyCombination { Key.Nvda, Key.L });

		/// <summary>
		/// Gets the command to reads any currently selected text.
		/// </summary>
		/// <value>
		/// The read current text selection command.
		/// </value>
		public static NvdaCommand ReadCurrentTextSelection => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.Shift, Key.UpArrow },
			new KeyCombination { Key.Nvda, Key.Shift, Key.S });

		/// <summary>
		/// Gets the command to moves the system caret to the next column (staying in the same row).
		/// </summary>
		/// <value>
		/// The move to next column command.
		/// </value>
		public static NvdaCommand MoveToNextColumn => new NvdaCommand(new KeyCombination { Key.Control, Key.LeftAlt, Key.RightArrow });

		/// <summary>
		/// Gets the command to moves the system caret to the previous column (staying in the same row).
		/// </summary>
		/// <value>
		/// The move to previous column command.
		/// </value>
		public static NvdaCommand MoveToPreviousColumn => new NvdaCommand(new KeyCombination { Key.Control, Key.LeftAlt, Key.RightArrow });

		/// <summary>
		/// Gets the command to moves the system caret to the next row (staying in the same column).
		/// </summary>
		/// <value>
		/// The move to next row command.
		/// </value>
		public static NvdaCommand MoveToNextRow => new NvdaCommand(new KeyCombination { Key.Control, Key.LeftAlt, Key.DownArrow });

		/// <summary>
		/// Gets the command to moves the system caret to the previous row (staying in the same column).
		/// </summary>
		/// <value>
		/// The move to previous row command.
		/// </value>
		public static NvdaCommand MoveToPreviousRow => new NvdaCommand(new KeyCombination { Key.Control, Key.LeftAlt, Key.UpArrow });
	}
}
