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
	/// Stores all settings related to vision category
	/// </summary>
	public class VisionSettings
	{
		/// <summary>
		/// Gets or sets a value indicating whether NVDA should highlight the system focus.
		/// Focus Highlight can help to identify the system focus position. This positions are highlighted with a colored rectangle outline.
		/// <list type="bullet">
		/// <item>Solid blue highlights a combined navigator object and system focus location (e.g. because the navigator object follows the system focus).</item>
		/// <item>Dashed blue highlights just the system focus object. Solid pink highlights just the navigator object.</item>
		/// </list>
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should highlight the focus; otherwise, <c>false</c>.
		/// </value>
		public bool HighlightFocus { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should highlight the navegator object position.
		/// Navegator object Highlight can help to identify the navigator object position.
		/// <list type="bullet">
		/// <item>Solid blue highlights a combined navigator object and system focus location (e.g. because the navigator object follows the system focus).</item>
		/// <item>Solid pink highlights the navigator object.</item>
		/// </list>
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should highlight the navigator object; otherwise, <c>false</c>.
		/// </value>
		public bool HighlightNavigator { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should highlight the browse mode possition.
		/// browse mode Highlight can help to identify the browse mode position. This position are highlighted with a colored rectangle outline
		/// (solid yellow highlights the virtual caret used in browse mode, where there is no physical caret such as in web browsers.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should highlight browse mode; otherwise, <c>false</c>.
		/// </value>
		public bool HighlightBrowseMode { get; set; }
	}
}