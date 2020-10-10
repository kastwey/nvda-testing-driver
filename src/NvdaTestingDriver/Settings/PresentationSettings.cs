// Copyright (C) 2020 Juan José Montiel
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
	/// Sets the presentation settings.
	/// </summary>
	public class PresentationSettings
	{
		/// <summary>
		/// Toggles the announcement of new content in particular objects such as terminals and the history control in chat programs.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should report dynamic content changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportDynamicContentChanges { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should report tool tips as they appear.
		/// Many Windows and controls show a small message (or tool tip) when you move the mouse
		/// pointer over them, or sometimes when you move the focus to them.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should report tooltips; otherwise, <c>false</c>.
		/// </value>
		public bool ReportTooltips { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should report help balloons as they appear.
		/// Help Balloons are like tool tips, but are usually larger in size, and are associated with system
		/// events such as a network cable being unplugged, or perhaps to alert you about Windows security issues.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should report help balloons; otherwise, <c>false</c>.
		/// </value>
		public bool ReportHelpBalloons { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should include the shortcut key that is
		/// associated with a certain object or control when it is reported.
		/// For example the File menu on a menu bar may have a shortcut key of alt+f.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should report keyboard shortcuts; otherwise, <c>false</c>.
		/// </value>
		public bool ReportKeyboardShortcuts { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether you wish to have an object's position
		/// (e.g. 1 of 4) reported when moving to the object with the focus or object navigation.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should report object position information; otherwise, <c>false</c>.
		/// </value>
		public bool ReportObjectPositionInformation { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether  NVDA should announce guess object position information when
		/// it is otherwise unavailable for a particular control.
		/// When true, NVDA will report position information for more controls such as menus and toolbars, however this information may be slightly inaccurate.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should report guess object position information when unavailable; otherwise, <c>false</c>.
		/// </value>
		public bool GuessObjectPositionInformationWhenUnavailable { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce the object description along with objects.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should report object descriptions; otherwise, <c>false</c>.
		/// </value>
		public bool ReportObjectDescriptions { get; set; }

		/// <summary>
		/// Gets or sets the progress bar updates settings.
		/// </summary>
		/// <value>
		/// The progress bar updates settings.
		/// </value>
		public ProgressBarUpdateSettings ProgressBarUpdates { get; set; }
	}
}
