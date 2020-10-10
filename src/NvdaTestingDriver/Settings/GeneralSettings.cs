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
	/// Class to store NVDA general settings
	/// </summary>
	public class GeneralSettings
	{
		/// <summary>
		/// Gets or sets the language.
		/// </summary>
		/// <value>
		/// The language.
		/// </value>
		public NvdaLanguage Language { get; set; } = NvdaLanguage.Arabic;

		/// <summary>
		/// Gets or sets a value indicating whether nvda should play start and exit sounds.
		/// </summary>
		/// <value>
		///   <c>true</c> if [play start and exit sounds]; otherwise, <c>false</c>.
		/// </value>
		public bool PlayStartAndExitSounds { get; set; }
	}
}