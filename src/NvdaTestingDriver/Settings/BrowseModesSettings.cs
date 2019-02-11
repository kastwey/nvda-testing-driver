// Copyright (C) 2019 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

namespace NvdaTestingDriver.Settings
{
	/// <summary>
	/// Sets the browse mode settings
	/// </summary>
	public class BrowseModesSettings
	{
		/// <summary>
		/// Gets or sets the value which allows you to specify whether content in browse mode should place content such as links and other fields on their own line,
		/// or if it should keep them in the flow of text as it is visually shown.
		/// If the option is enabled then things will stay as they are visually shown, but if it is disabled
		/// then fields will be placed on their own line.
		/// </summary>
		/// <value>
		///   <c>true</c> if [use screen layout]; otherwise, <c>false</c>.
		/// </value>
		public bool UseScreenLayout { get; set; }

		/// <summary>
		/// This checkbox toggles the automatic reading of a page after it loads in browse mode. This option is enabled by default.
		/// </summary>
		/// <value>
		///   <c>true</c> if [automatic say all on page load]; otherwise, <c>false</c>.
		/// </value>
		public bool AutoSayAllOnPageLoad { get; set; }

		/// <summary>
		/// This option allows focus mode to be invoked if focus changes. For example, when on a
		/// web page, if you press tab and you land on a form, if this option is checked, focus mode will automatically be invoked.
		/// </summary>
		/// <value>
		///   <c>true</c> if [automatic pass through on focus change]; otherwise, <c>false</c>.
		/// </value>
		public bool AutoPassThroughOnFocusChange { get; set; }

		/// <summary>
		/// This option, when checked, allows NVDA to enter and leave focus mode when using arrow keys. For example, if arrowing down a web page and you land on an
		/// edit box, NVDA will automatically bring you into focus mode.
		/// If you arrow out of the edit box, NVDA will put you back in browse mode.
		/// </summary>
		/// <value>
		///   <c>true</c> if [automatic pass through on caret move]; otherwise, <c>false</c>.
		/// </value>
		public bool AutoPassThroughOnCaretMove { get; set; }

		/// <summary>
		/// Enabled by default, this option allows you to choose if gestures (such as key presses)
		/// that do not result in an NVDA command and are not considered to be a command key
		/// in general, should be trapped from going through to the document you are currently focused on.
		/// As an example, if enabled, if the letter j was pressed, it would be trapped from reaching the document,
		/// even though it is not a quick navigation command nor is it likely to be a command in the application itself.
		/// </summary>
		/// <value>
		///   <c>true</c> if [trap non command gestures]; otherwise, <c>false</c>.
		/// </value>
		public bool TrapNonCommandGestures { get; set; }

		/// <summary>
		/// This field sets the maximum length of a line in browse mode (in characters).
		/// </summary>
		/// <value>
		/// The maximum length of the line.
		/// </value>
		public int MaxLineLength { get; set; }

		/// <summary>
		/// This field sets the amount of lines you will move by when pressing page up or page down while in browse mode.
		/// </summary>
		/// <value>
		/// The maximum length of the line.
		/// </value>
		public int LinesPerPage { get; set; }
	}
}
