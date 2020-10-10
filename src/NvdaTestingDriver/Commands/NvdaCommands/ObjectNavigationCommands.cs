// Copyright (C) 2020 Juan José Montiel
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
	/// Group all NVDA commands under object navigation category
	/// </summary>
	public static class ObjectNavigationCommands
	{
		/// <summary>
		/// Gets the command to reports the current navigator object.
		/// </summary>
		/// <value>
		/// The report current object command.
		/// </value>
		public static NvdaCommand ReportCurrentObject => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPad5 },
			new KeyCombination { Key.Nvda, Key.Shift, Key.O });

		/// <summary>
		/// Gets the command to moves to the object containing the current navigator object.
		/// </summary>
		/// <value>
		/// The move to containing object command.
		/// </value>
		public static NvdaCommand MoveToContainingObject => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPad8 },
			new KeyCombination { Key.Nvda, Key.Shift, Key.UpArrow });

		/// <summary>
		/// Gets the command to moves to the object before the current navigator object.
		/// </summary>
		/// <value>
		/// The move to previous object command.
		/// </value>
		public static NvdaCommand MoveToPreviousObject => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPad4 },
			new KeyCombination { Key.Nvda, Key.Shift, Key.LeftArrow });

		/// <summary>
		/// Gets the command to moves to the object after the current navigator object.
		/// </summary>
		/// <value>
		/// The move to next object command.
		/// </value>
		public static NvdaCommand MoveToNextObject => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPad6 },
			new KeyCombination { Key.Nvda, Key.Shift, Key.RightArrow });

		/// <summary>
		/// Gets the command to moves to the first object contained by the current navigator object.
		/// </summary>
		/// <value>
		/// The move to first contained object command.
		/// </value>
		public static NvdaCommand MoveToFirstContainedObject => new NvdaCommand(
			 new KeyCombination { Key.Nvda, Key.NumPad2 },
			 new KeyCombination { Key.Nvda, Key.Shift, Key.DownArrow });

		/// <summary>
		/// Gets the command to moves to the object that currently has the system focus, and also places the review cursor at the position of the System caret, if it is showing.
		/// </summary>
		/// <value>
		/// The move to focus object command.
		/// </value>
		public static NvdaCommand MoveToFocusObject => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPadMinus },
			new KeyCombination { Key.Nvda, Key.Backspace });

		/// <summary>
		/// Gets the command to activates the current navigator object (similar to clicking with the mouse or pressing space when it has the system focus).
		/// </summary>
		/// <value>
		/// The activate current navigator object command.
		/// </value>
		public static NvdaCommand ActivateCurrentNavigatorObject => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPadEnter },
			new KeyCombination { Key.Nvda, Key.NumPadEnter });

		/// <summary>
		/// Gets the command to moves the System focus to the current navigator object.
		/// </summary>
		/// <value>
		/// The move system focus to current review position command.
		/// </value>
		public static NvdaCommand MoveSystemFocusToCurrentReviewPosition => new NvdaCommand(
				 new KeyCombination { Key.Nvda, Key.Shift, Key.NumPadMinus },
				 new KeyCombination { Key.Nvda, Key.Shift, Key.Backspace });

		/// <summary>
		/// Gets the command to moves the caret to the current navigator object.
		/// </summary>
		/// <value>
		/// The move caret to current review position command.
		/// </value>
		public static NvdaCommand MoveCaretToCurrentReviewPosition => new NvdaCommand(
			new List<KeyCombination>
			{
				new KeyCombination { Key.Nvda, Key.Shift, Key.NumPadMinus },
				new KeyCombination { Key.Nvda, Key.Shift, Key.NumPadMinus },
			},
			new List<KeyCombination>
			{
				new KeyCombination { Key.Nvda, Key.Shift, Key.Backspace },
				new KeyCombination { Key.Nvda, Key.Shift, Key.Backspace },
			});

		/// <summary>
		/// Gets the command to reports information about the location of the text or object at the review cursor. For example, this might include the percentage through the document, the distance from the edge of the page or the exact screen position.
		/// </summary>
		/// <value>
		/// The report review cursor location command.
		/// </value>
		public static NvdaCommand ReportReviewCursorLocation => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.NumPadDelete },
			new KeyCombination { Key.Nvda, Key.Delete });
	}
}
