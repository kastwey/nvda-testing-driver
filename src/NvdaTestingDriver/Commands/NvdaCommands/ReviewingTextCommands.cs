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
	/// Class which group all reviewing text commands
	/// </summary>
	public static class ReviewingTextCommands
	{
		/// <summary>
		/// Gets the command to moves the review cursor to the top line of the text.
		/// </summary>
		/// <value>
		/// The move to top line in review command.
		/// </value>
		public static NvdaCommand MoveToTopLineInReview => new NvdaCommand(
			new KeyCombination { Key.Shift, Key.Oem7 },
			new KeyCombination { Key.Nvda, Key.Control, Key.Home });

		/// <summary>
		/// Gets the command to moves the review cursor to the previous line of text.
		/// </summary>
		/// <value>
		/// The move to previous line in review command.
		/// </value>
		public static NvdaCommand MoveToPreviousLineInReview => new NvdaCommand(
			new KeyCombination { Key.Oem7 },
			new KeyCombination { Key.Nvda, Key.UpArrow });

		/// <summary>
		/// Gets the command to announces the current line of text where the review cursor is positioned.
		/// </summary>
		/// <value>
		/// The report current line in review command.
		/// </value>
		public static NvdaCommand ReportCurrentLineInReview => new NvdaCommand(
			new KeyCombination { Key.NumPad8 },
			new KeyCombination { Key.Nvda, Key.Shift, Key.OemPeriod });

		/// <summary>
		/// Gets the command to move the review cursor to the next line of text.
		/// </summary>
		/// <value>
		/// The move to next line in review command.
		/// </value>
		public static NvdaCommand MoveToNextLineInReview => new NvdaCommand(
			new KeyCombination { Key.NumPad9 },
			new KeyCombination { Key.Nvda, Key.DownArrow });

		/// <summary>
		/// Gets the command to moves the review cursor to the bottom line of text.
		/// </summary>
		/// <value>
		/// The move to bottom line in review command.
		/// </value>
		public static NvdaCommand MoveToBottomLineInReview => new NvdaCommand(
			new KeyCombination { Key.Shift, Key.NumPad9 },
			new KeyCombination { Key.Nvda, Key.Control, Key.End });

		/// <summary>
		/// Gets the command to moves the review cursor to the previous word in the text.
		/// </summary>
		/// <value>
		/// The move to previous word in review command.
		/// </value>
		public static NvdaCommand MoveToPreviousWordInReview => new NvdaCommand(
			new KeyCombination { Key.NumPad4 },
			new KeyCombination { Key.Nvda, Key.Control, Key.LeftArrow });

		/// <summary>
		/// Gets the command to announces the current word in the text where the review cursor is positioned.
		/// </summary>
		/// <value>
		/// The report current word in review command.
		/// </value>
		public static NvdaCommand ReportCurrentWordInReview => new NvdaCommand(
			new KeyCombination { Key.NumPad5 },
			new KeyCombination { Key.Nvda, Key.Control, Key.OemPeriod });

		/// <summary>
		/// Gets the command to move the review cursor to the next word in the text.
		/// </summary>
		/// <value>
		/// The move to next word in review command.
		/// </value>
		public static NvdaCommand MoveToNextWordInReview => new NvdaCommand(
			new KeyCombination { Key.NumPad6 },
			new KeyCombination { Key.Nvda, Key.Control, Key.RightArrow });

		/// <summary>
		/// Gets the command to moves the review cursor to the start of the current line in the text.
		/// </summary>
		/// <value>
		/// The move to start of line in review command.
		/// </value>
		public static NvdaCommand MoveToStartOfLineInReview => new NvdaCommand(
			new KeyCombination { Key.Shift, Key.NumPad1 },
			new KeyCombination { Key.Nvda, Key.Home });

		/// <summary>
		/// Gets the command to moves the review cursor to the previous character on the current line in the text.
		/// </summary>
		/// <value>
		/// The move to previous character in review command.
		/// </value>
		public static NvdaCommand MoveToPreviousCharacterInReview => new NvdaCommand(
			new KeyCombination { Key.NumPad1 },
			new KeyCombination { Key.Nvda, Key.LeftArrow });

		/// <summary>
		/// Gets the command to announces the current character on the line of text where the review cursor is positioned.
		/// </summary>
		/// <value>
		/// The report current character in review command.
		/// </value>
		public static NvdaCommand ReportCurrentCharacterInReview => new NvdaCommand(
			new KeyCombination { Key.NumPad2 },
			new KeyCombination { Key.Nvda, Key.OemPeriod });

		/// <summary>
		/// Gets the command to move the review cursor to the next character on the current line of text.
		/// </summary>
		/// <value>
		/// The move to next character in review command.
		/// </value>
		public static NvdaCommand MoveToNextCharacterInReview => new NvdaCommand(
			new KeyCombination { Key.NumPad3 },
			new KeyCombination { Key.Nvda, Key.RightArrow });

		/// <summary>
		/// Gets the command to moves the review cursor to the end of the current line of text.
		/// </summary>
		/// <value>
		/// The move to end of line in review command.
		/// </value>
		public static NvdaCommand MoveToEndOfLineInReview => new NvdaCommand(
			new KeyCombination { Key.Shift, Key.NumPad3 },
			new KeyCombination { Key.Nvda, Key.End });

		/// <summary>
		/// Gets the command to reads from the current position of the review cursor, moving it as it goes.
		/// </summary>
		/// <value>
		/// The say all in review command.
		/// </value>
		public static NvdaCommand SayAllInReview => new NvdaCommand(
			new KeyCombination { Key.NumPadPlus },
			new KeyCombination { Key.Nvda, Key.Shift, Key.A });

		/// <summary>
		/// Gets the command to starts the select then copy process from the current position of the review cursor.
		/// The actual action is not performed until you tell NVDA where the end of the text range is
		/// </summary>
		/// <value>
		/// The select then copy from review cursor command.
		/// </value>
		public static NvdaCommand SelectThenCopyFromReviewCursor => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.F9 });

		/// <summary>
		/// Gets the command to on the first press, text is selected from the position previously set start marker up to
		/// and including the review cursor's current position.
		/// After pressing this key a second time, the text will be copied to the Windows clipboard
		/// </summary>
		/// <value>
		/// The select then copy to review cursor command.
		/// </value>
		public static NvdaCommand SelectThenCopyToReviewCursor => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.F10 });

		/// <summary>
		/// Gets the command to reports the formatting of the text where the review cursor is currently situated.
		/// </summary>
		/// <value>
		/// The report text formating command.
		/// </value>
		public static NvdaCommand ReportTextFormating => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.F });
	}
}
