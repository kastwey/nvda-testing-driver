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
	/// Sets the review cursor settings
	/// </summary>
	public class ReviewCursorSettings
	{
		/// <summary>
		/// Gets or sets a value indicating whether The review cursor should always be placed in the same object as the current system focus whenever the focus changes.
		/// </summary>
		/// <value>
		///   <c>true</c> if review cursor should follow always focus; otherwise, <c>false</c>.
		/// </value>
		public bool FollowFocus { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the review cursor should automatically be moved to the position of the System caret each time it moves.
		/// </summary>
		/// <value>
		///   <c>true</c> if  review cursor should follow caret; otherwise, <c>false</c>.
		/// </value>
		public bool FollowCaret { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the review cursor will follow the mouse as it moves.
		/// </summary>
		/// <value>
		///   <c>true</c> if the review cursor should follow mouse; otherwise, <c>false</c>.
		/// </value>
		public bool FollowMouse { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should filter the hierarchy of
		/// objects that can be navigated to exclude objects that aren't of interest to the user;
		/// e.g. invisible objects and objects used only for layout purposes.
		/// </summary>
		/// <value>
		///   <c>true</c> if  you want to active the simple review mode]; otherwise, <c>false</c>.
		/// </value>
		public bool SimpleReviewMode { get; set; }
	}
}
