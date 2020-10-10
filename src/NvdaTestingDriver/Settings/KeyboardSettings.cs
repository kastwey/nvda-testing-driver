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
	/// Sets the keyboard settings
	/// </summary>
	public class KeyboardSettings
	{
		/// <summary>
		/// lets you choose what type of keyboard layout NVDA should use. Currently the two that come with NVDA are Desktop and Laptop.
		/// </summary>
		/// <value>
		/// The keyboard layout.
		/// </value>
		public KeyboardLayout KeyboardLayout { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether capslock can be used as an NVDA modifier key.
		/// </summary>
		/// <value>
		///   <c>true</c> if capslock can be used as an NVDA modifier key; otherwise, <c>false</c>.
		/// </value>
		public bool UseCapsLockAsNVDAModifierKey { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether  NVDA will announce all characters you type on the keyboard.
		/// </summary>
		/// <value>
		///   <c>true</c> if  NVDA should speak typed characters; otherwise, <c>false</c>.
		/// </value>
		public bool SpeakTypedCharacters { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA will announce all words you type on the keyboard.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should speak typed words; otherwise, <c>false</c>.
		/// </value>
		public bool SpeakTypedWords { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether speech will be interrupted each time the Enter key is pressed. This is on by default.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA interrupt speech for enter; otherwise, <c>false</c>.
		/// </value>
		public bool SpeechInterruptForEnter { get; set; }
	}
}
