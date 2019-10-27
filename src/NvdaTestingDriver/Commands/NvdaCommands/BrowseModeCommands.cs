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
	/// Group all browse mode commands</summary>
	public static class BrowseModeCommands
	{
		/// <summary>
		/// Gets the command to toggle between focus mode and browse mode.
		/// </summary>
		/// <value>
		/// The toggle browse and focus mode command.
		/// </value>
		public static NvdaCommand ToggleBrowseAndFocusMode => new NvdaCommand(new KeyCombination { Key.Nvda, Key.Space }, nameof(ToggleBrowseAndFocusMode));

		/// <summary>
		/// Gets the command to switch back to browse mode if focus mode was previously switched to automatically.
		/// </summary>
		/// <value>
		/// The exit focus mode command.
		/// </value>
		public static NvdaCommand ExitFocusMode => new NvdaCommand(new KeyCombination { Key.Escape });

		/// <summary>
		/// Gets the command to reload the current document content (useful if certain content seems to be missing from the document. Not available in Microsoft Word and Outlook.).
		/// </summary>
		/// <value>
		/// The refresh browse mode document command.
		/// </value>
		public static NvdaCommand RefreshBrowseModeDocument => new NvdaCommand(new KeyCombination { Key.Nvda, Key.F5 });

		/// <summary>
		/// Gets the command to pop up a dialog in which you can type some text to find in the current document.
		/// </summary>
		/// <value>
		/// The find command.
		/// </value>
		public static NvdaCommand Find => new NvdaCommand(new KeyCombination { Key.Nvda, Key.Control, Key.F });

		/// <summary>
		/// Gets the command to find the next occurrence of the text in the document that you previously searched for.
		/// </summary>
		/// <value>
		/// The find next command.
		/// </value>
		public static NvdaCommand FindNext => new NvdaCommand(new KeyCombination { Key.Nvda, Key.F3 });

		/// <summary>
		/// Gets the command to find the previous occurrence of the text in the document you previously searched for.
		/// </summary>
		/// <value>
		/// The find previous.
		/// </value>
		public static NvdaCommand FindPrevious => new NvdaCommand(new KeyCombination { Key.Nvda, Key.Shift, Key.F3 });

		/// <summary>
		/// Gets the command to open a new window containing a long description for the element you are on if it has one.
		/// </summary>
		/// <value>
		/// The open long description command.
		/// </value>
		public static NvdaCommand OpenLongDescription => new NvdaCommand(new KeyCombination { Key.Nvda, Key.D });

		/// <summary>
		/// Gets a command to go to the next heading.
		/// </summary>
		/// <value>
		/// The command to go to the next heading.
		/// </value>
		public static NvdaCommand NextHeading => new NvdaCommand(new KeyCombination { Key.H });

		/// <summary>
		/// Gets a command to go to the next heading level 1.
		/// </summary>
		/// <value>
		/// The next heading1 command.
		/// </value>
		public static NvdaCommand NextHeading1 => new NvdaCommand(new KeyCombination { Key.D1 });

		/// <summary>
		/// Gets the command to go to the next heading level 2.
		/// </summary>
		/// <value>
		/// The next heading2 command.
		/// </value>
		public static NvdaCommand NextHeading2 => new NvdaCommand(new KeyCombination { Key.D2 });

		/// <summary>
		/// Gets the command to go to the next heading level 3.
		/// </summary>
		/// <value>
		/// The next heading3 command.
		/// </value>
		public static NvdaCommand NextHeading3 => new NvdaCommand(new KeyCombination { Key.D3 });

		/// <summary>
		/// Gets hte command to go to the next heading level 4.
		/// </summary>
		/// <value>
		/// The next heading4 command.
		/// </value>
		public static NvdaCommand NextHeading4 => new NvdaCommand(new KeyCombination { Key.D4 });

		/// <summary>
		/// Gets the command to Go to the next heading level 5.
		/// </summary>
		/// <value>
		/// The next heading5 command.
		/// </value>
		public static NvdaCommand NextHeading5 => new NvdaCommand(new KeyCombination { Key.D5 });

		/// <summary>
		/// Gets the command to go to the next heading level 6.
		/// </summary>
		/// <value>
		/// The next heading6 command.
		/// </value>
		public static NvdaCommand NextHeading6 => new NvdaCommand(new KeyCombination { Key.D6 });

		/// <summary>
		/// Gets the command to go to the previous heading.
		/// </summary>
		/// <value>
		/// The previous heading command.
		/// </value>
		public static NvdaCommand PreviousHeading => new NvdaCommand(new KeyCombination { Key.Shift, Key.H });

		/// <summary>
		/// Gets the command to go to the previous heading level 1.
		/// </summary>
		/// <value>
		/// The previous heading1 command.
		/// </value>
		public static NvdaCommand PreviousHeading1 => new NvdaCommand(new KeyCombination { Key.Shift, Key.D1 });

		/// <summary>
		/// Gets the command to go to the previous heading level 2.
		/// </summary>
		/// <value>
		/// The previous heading2 command.
		/// </value>
		public static NvdaCommand PreviousHeading2 => new NvdaCommand(new KeyCombination { Key.Shift, Key.D2 });

		/// <summary>
		/// Gets the command to go to the previous heading level 3.
		/// </summary>
		/// <value>
		/// The previous heading3 command.
		/// </value>
		public static NvdaCommand PreviousHeading3 => new NvdaCommand(new KeyCombination { Key.Shift, Key.D3 });

		/// <summary>
		/// Gets the command to go to the previous heading level 4.
		/// </summary>
		/// <value>
		/// The previous heading4 command.
		/// </value>
		public static NvdaCommand PreviousHeading4 => new NvdaCommand(new KeyCombination { Key.Shift, Key.D4 });

		/// <summary>
		/// Gets the command to go to the previous heading level 5.
		/// </summary>
		/// <value>
		/// The previous heading5 command.
		/// </value>
		public static NvdaCommand PreviousHeading5 => new NvdaCommand(new KeyCombination { Key.Shift, Key.D5 });

		/// <summary>
		/// Gets the command to go to the previous heading level 6.
		/// </summary>
		/// <value>
		/// The previous heading6 command.
		/// </value>
		public static NvdaCommand PreviousHeading6 => new NvdaCommand(new KeyCombination { Key.Shift, Key.D6 });

		/// <summary>
		/// Gets the command to go to the next list.
		/// </summary>
		/// <value>
		/// The next list command.
		/// </value>
		public static NvdaCommand NextList => new NvdaCommand(new KeyCombination { Key.L });

		/// <summary>
		/// Gets the command to go to the previous list.
		/// </summary>
		/// <value>
		/// The previous list command.
		/// </value>
		public static NvdaCommand PreviousList => new NvdaCommand(new KeyCombination { Key.Shift, Key.L });

		/// <summary>
		/// Gets the command to o to the next list item.
		/// </summary>
		/// <value>
		/// The next list item command.
		/// </value>
		public static NvdaCommand NextListItem => new NvdaCommand(new KeyCombination { Key.I });

		/// <summary>
		/// Gets the command to go to the previous list item.
		/// </summary>
		/// <value>
		/// The previous list item command.
		/// </value>
		public static NvdaCommand PreviousListItem => new NvdaCommand(new KeyCombination { Key.Shift, Key.I });

		/// <summary>
		/// Gets the command to go to the next table.
		/// </summary>
		/// <value>
		/// The next table command.
		/// </value>
		public static NvdaCommand NextTable => new NvdaCommand(new KeyCombination { Key.T });

		/// <summary>
		/// Gets the command to go to the previous table.
		/// </summary>
		/// <value>
		/// The previous table command.
		/// </value>
		public static NvdaCommand PreviousTable => new NvdaCommand(new KeyCombination { Key.Shift, Key.T });

		/// <summary>
		/// Gets the command to go to the next link.
		/// </summary>
		/// <value>
		/// The next link command.
		/// </value>
		public static NvdaCommand NextLink => new NvdaCommand(new KeyCombination { Key.K });

		/// <summary>
		/// Gets the command to go to the previous link.
		/// </summary>
		/// <value>
		/// The previous link value.
		/// </value>
		public static NvdaCommand PreviousLink => new NvdaCommand(new KeyCombination { Key.Shift, Key.K });

		/// <summary>
		/// Gets the command to go to the next non linked text.
		/// </summary>
		/// <value>
		/// The next non linked text command.
		/// </value>
		public static NvdaCommand NextNonLinkedText => new NvdaCommand(new KeyCombination { Key.N });

		/// <summary>
		/// Gets the command to go to the previous non linked text.
		/// </summary>
		/// <value>
		/// The previous non linked text command.
		/// </value>
		public static NvdaCommand PreviousNonLinkedText => new NvdaCommand(new KeyCombination { Key.Shift, Key.N });

		/// <summary>
		/// Gets the command to go to the next form field.
		/// </summary>
		/// <value>
		/// The next form field command.
		/// </value>
		public static NvdaCommand NextFormField => new NvdaCommand(new KeyCombination { Key.F });

		/// <summary>
		/// Gets the command to go to the previous form field.
		/// </summary>
		/// <value>
		/// The previous form field command.
		/// </value>
		public static NvdaCommand PreviousFormField => new NvdaCommand(new KeyCombination { Key.Shift, Key.F });

		/// <summary>
		/// Gets the command to go to the next unvisited link.
		/// </summary>
		/// <value>
		/// The next unvisited link command.
		/// </value>
		public static NvdaCommand NextUnvisitedLink => new NvdaCommand(new KeyCombination { Key.U });

		/// <summary>
		/// Gets the command to go to the previous unvisited link.
		/// </summary>
		/// <value>
		/// The previous unvisited link.
		/// </value>
		public static NvdaCommand PreviousUnvisitedLink => new NvdaCommand(new KeyCombination { Key.Shift, Key.U });

		/// <summary>
		/// Gets the command to go to the next visited link.
		/// </summary>
		/// <value>
		/// The next visited link command.
		/// </value>
		public static NvdaCommand NextVisitedLink => new NvdaCommand(new KeyCombination { Key.V });

		/// <summary>
		/// Gets the command to go to the previous visited link.
		/// </summary>
		/// <value>
		/// The previous visited link command.
		/// </value>
		public static NvdaCommand PreviousVisitedLink => new NvdaCommand(new KeyCombination { Key.Shift, Key.V });

		/// <summary>
		/// Gets the command to go to the next edit field.
		/// </summary>
		/// <value>
		/// The next edit field command.
		/// </value>
		public static NvdaCommand NextEditField => new NvdaCommand(new KeyCombination { Key.E });

		/// <summary>
		/// Gets the command to go to the previous edit field.
		/// </summary>
		/// <value>
		/// The previous edit field command.
		/// </value>
		public static NvdaCommand PreviousEditField => new NvdaCommand(new KeyCombination { Key.Shift, Key.E });

		/// <summary>
		/// Gets the command to go to the next button.
		/// </summary>
		/// <value>
		/// The next button command.
		/// </value>
		public static NvdaCommand NextButton => new NvdaCommand(new KeyCombination { Key.B });

		/// <summary>
		/// Gets the command to go to the previous button.
		/// </summary>
		/// <value>
		/// The previous button command.
		/// </value>
		public static NvdaCommand PreviousButton => new NvdaCommand(new KeyCombination { Key.Shift, Key.B });

		/// <summary>
		/// Gets the command to go to the next checkbox.
		/// </summary>
		/// <value>
		/// The next checkbox command.
		/// </value>
		public static NvdaCommand NextCheckbox => new NvdaCommand(new KeyCombination { Key.X });

		/// <summary>
		/// Gets the command to go to the previous checkbox.
		/// </summary>
		/// <value>
		/// The previous checkbox command.
		/// </value>
		public static NvdaCommand PreviousCheckbox => new NvdaCommand(new KeyCombination { Key.Shift, Key.X });

		/// <summary>
		/// Gets the command to go to the next combo box.
		/// </summary>
		/// <value>
		/// The next ComboBox command.
		/// </value>
		public static NvdaCommand NextComboBox => new NvdaCommand(new KeyCombination { Key.C });

		/// <summary>
		/// Gets the command to go to the previous combo box.
		/// </summary>
		/// <value>
		/// The previous ComboBox command.
		/// </value>
		public static NvdaCommand PreviousComboBox => new NvdaCommand(new KeyCombination { Key.Shift, Key.C });

		/// <summary>
		/// Gets the command to go to the next radio button.
		/// </summary>
		/// <value>
		/// The next RadioButton command.
		/// </value>
		public static NvdaCommand NextRadioButton => new NvdaCommand(new KeyCombination { Key.R });

		/// <summary>
		/// Gets the command to go to the previous radio button.
		/// </summary>
		/// <value>
		/// The previous RadioButton command.
		/// </value>
		public static NvdaCommand PreviousRadioButton => new NvdaCommand(new KeyCombination { Key.Shift, Key.R });

		/// <summary>
		/// Gets the command to  go to the next block quote.
		/// </summary>
		/// <value>
		/// The next block quote command.
		/// </value>
		public static NvdaCommand NextBlockQuote => new NvdaCommand(new KeyCombination { Key.Q });

		/// <summary>
		/// Gets the command to go to the previous block quote.
		/// </summary>
		/// <value>
		/// The previous block quote command.
		/// </value>
		public static NvdaCommand PreviousBlockQuote => new NvdaCommand(new KeyCombination { Key.Shift, Key.Q });

		/// <summary>
		/// Gets the command to go to the next separator.
		/// </summary>
		/// <value>
		/// The next separator command.
		/// </value>
		public static NvdaCommand NextSeparator => new NvdaCommand(new KeyCombination { Key.S });

		/// <summary>
		/// Gets the command to go to the previous separator.
		/// </summary>
		/// <value>
		/// The previous separator command.
		/// </value>
		public static NvdaCommand PreviousSeparator => new NvdaCommand(new KeyCombination { Key.Shift, Key.S });

		/// <summary>
		/// Gets the command to go to the next frame.
		/// </summary>
		/// <value>
		/// The next frame command.
		/// </value>
		public static NvdaCommand NextFrame => new NvdaCommand(new KeyCombination { Key.M });

		/// <summary>
		/// Gets the command to go to the previous frame.
		/// </summary>
		/// <value>
		/// The previous frame command.
		/// </value>
		public static NvdaCommand PreviousFrame => new NvdaCommand(new KeyCombination { Key.Shift, Key.M });

		/// <summary>
		/// Gets the command to go to the next graphic.
		/// </summary>
		/// <value>
		/// The next graphic command.
		/// </value>
		public static NvdaCommand NextGraphic => new NvdaCommand(new KeyCombination { Key.G });

		/// <summary>
		/// Gets the command to go to the previous graphic.
		/// </summary>
		/// <value>
		/// The previous graphic command.
		/// </value>
		public static NvdaCommand PreviousGraphic => new NvdaCommand(new KeyCombination { Key.Shift, Key.G });

		/// <summary>
		/// Gets the command to go to the next landmark.
		/// </summary>
		/// <value>
		/// The next landmark command.
		/// </value>
		public static NvdaCommand NextLandmark => new NvdaCommand(new KeyCombination { Key.D });

		/// <summary>
		/// Gets the command to go to the previous landmark.
		/// </summary>
		/// <value>
		/// The previous landmark command.
		/// </value>
		public static NvdaCommand PreviousLandmark => new NvdaCommand(new KeyCombination { Key.Shift, Key.D });

		/// <summary>
		/// Gets the command to go to the next embedded object (audio and video player, application, dialog, etc.).
		/// </summary>
		/// <value>
		/// The next embedded object command.
		/// </value>
		public static NvdaCommand NextEmbeddedObject => new NvdaCommand(new KeyCombination { Key.O });

		/// <summary>
		/// Gets the command to go to the previous embedded object (audio and video player, application, dialog, etc.).
		/// </summary>
		/// <value>
		/// The previous embedded object command.
		/// </value>
		public static NvdaCommand PreviousEmbeddedObject => new NvdaCommand(new KeyCombination { Key.Shift, Key.O });

		/// <summary>
		/// Gets the command to go to the next annotation.
		/// </summary>
		/// <value>
		/// The next annotation command.
		/// </value>
		public static NvdaCommand NextAnnotation => new NvdaCommand(new KeyCombination { Key.A });

		/// <summary>
		/// Gets the command to go to the previous annotation.
		/// </summary>
		/// <value>
		/// The previous annotation command.
		/// </value>
		public static NvdaCommand PreviousAnnotation => new NvdaCommand(new KeyCombination { Key.Shift, Key.A });

		/// <summary>
		/// Gets the command to go to the next spelling error.
		/// </summary>
		/// <value>
		/// The next spelling error command.
		/// </value>
		public static NvdaCommand NextSpellingError => new NvdaCommand(new KeyCombination { Key.W });

		/// <summary>
		/// Gets the command to go to the previous spelling error.
		/// </summary>
		/// <value>
		/// The previous spelling error command.
		/// </value>
		public static NvdaCommand PreviousSpellingError => new NvdaCommand(new KeyCombination { Key.Shift, Key.W });
	}
	}
