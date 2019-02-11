// Copyright (C) 2019 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

namespace NvdaTestingDriver
{
	/// <summary>
	/// Class to store the information of a key, ready to be passed to NVDA Remote.
	/// </summary>
	public class Key
	{
		/// <summary>
		/// Gets key Aa.
		/// </summary>
		/// <value>
		/// the A key.
		/// </value>
		public static Key A => new Key { KeyCode = 65, ScanCode = 30, Extended = false };

		/// <summary>
		/// Gets the b key.
		/// </summary>
		/// <value>
		/// The B key.
		/// </value>
		public static Key B => new Key { KeyCode = 66, ScanCode = 48, Extended = false };

		/// <summary>
		/// Gets the c key.
		/// </summary>
		/// <value>
		/// The C key.
		/// </value>
		public static Key C => new Key { KeyCode = 67, ScanCode = 46, Extended = false };

		/// <summary>
		/// Gets the d key.
		/// </summary>
		/// <value>
		/// The d key.
		/// </value>
		public static Key D => new Key { KeyCode = 68, ScanCode = 32, Extended = false };

		/// <summary>
		/// Gets the e key.
		/// </summary>
		/// <value>
		/// The e key.
		/// </value>
		public static Key E => new Key { KeyCode = 69, ScanCode = 18, Extended = false };

		/// <summary>
		/// Gets the f key.
		/// </summary>
		/// <value>
		/// The f key.
		/// </value>
		public static Key F => new Key { KeyCode = 70, ScanCode = 33, Extended = false };

		/// <summary>
		/// Gets the g key.
		/// </summary>
		/// <value>
		/// The g key.
		/// </value>
		public static Key G => new Key { KeyCode = 71, ScanCode = 34, Extended = false };

		/// <summary>
		/// Gets the h key.
		/// </summary>
		/// <value>
		/// The h key.
		/// </value>
		public static Key H => new Key { KeyCode = 72, ScanCode = 35, Extended = false };

		/// <summary>
		/// Gets the i key.
		/// </summary>
		/// <value>
		/// The i key.
		/// </value>
		public static Key I => new Key { KeyCode = 73, ScanCode = 23, Extended = false };

		/// <summary>
		/// Gets the j key.
		/// </summary>
		/// <value>
		/// The j key.
		/// </value>
		public static Key J => new Key { KeyCode = 74, ScanCode = 36, Extended = false };

		/// <summary>
		/// Gets the k key.
		/// </summary>
		/// <value>
		/// The k key.
		/// </value>
		public static Key K => new Key { KeyCode = 75, ScanCode = 37, Extended = false };

		/// <summary>
		/// Gets the l key.
		/// </summary>
		/// <value>
		/// The l key.
		/// </value>
		public static Key L => new Key { KeyCode = 76, ScanCode = 38, Extended = false };

		/// <summary>
		/// Gets the m key.
		/// </summary>
		/// <value>
		/// The m key.
		/// </value>
		public static Key M => new Key { KeyCode = 77, ScanCode = 50, Extended = false };

		/// <summary>
		/// Gets the n key.
		/// </summary>
		/// <value>
		/// The n key.
		/// </value>
		public static Key N => new Key { KeyCode = 78, ScanCode = 49, Extended = false };

		/// <summary>
		/// Gets the ñ key.
		/// </summary>
		/// <value>
		/// The ñ key.
		/// </value>
		public static Key Ñ => new Key { KeyCode = 192, ScanCode = 39, Extended = false };

		/// <summary>
		/// Gets the o key.
		/// </summary>
		/// <value>
		/// The o key.
		/// </value>
		public static Key O => new Key { KeyCode = 79, ScanCode = 24, Extended = false };

		/// <summary>
		/// Gets the p key.
		/// </summary>
		/// <value>
		/// The p key.
		/// </value>
		public static Key P => new Key { KeyCode = 80, ScanCode = 25, Extended = false };

		/// <summary>
		/// Gets the q key.
		/// </summary>
		/// <value>
		/// The q  key.
		/// </value>
		public static Key Q => new Key { KeyCode = 81, ScanCode = 16, Extended = false };

		/// <summary>
		/// Gets the r key.
		/// </summary>
		/// <value>
		/// The r  key.
		/// </value>
		public static Key R => new Key { KeyCode = 82, ScanCode = 19, Extended = false };

		/// <summary>
		/// Gets the s key.
		/// </summary>
		/// <value>
		/// The s key.
		/// </value>
		public static Key S => new Key { KeyCode = 83, ScanCode = 31, Extended = false };

		/// <summary>
		/// Gets the t key.
		/// </summary>
		/// <value>
		/// The t key.
		/// </value>
		public static Key T => new Key { KeyCode = 84, ScanCode = 20, Extended = false };

		/// <summary>
		/// Gets the u key.
		/// </summary>
		/// <value>
		/// The u key.
		/// </value>
		public static Key U => new Key { KeyCode = 85, ScanCode = 22, Extended = false };

		/// <summary>
		/// Gets the v key.
		/// </summary>
		/// <value>
		/// The v key.
		/// </value>
		public static Key V => new Key { KeyCode = 86, ScanCode = 47, Extended = false };

		/// <summary>
		/// Gets the w key.
		/// </summary>
		/// <value>
		/// The w key.
		/// </value>
		public static Key W => new Key { KeyCode = 87, ScanCode = 17, Extended = false };

		/// <summary>
		/// Gets the x key.
		/// </summary>
		/// <value>
		/// The x key.
		/// </value>
		public static Key X => new Key { KeyCode = 88, ScanCode = 45, Extended = false };

		/// <summary>
		/// Gets the y key.
		/// </summary>
		/// <value>
		/// The y key.
		/// </value>
		public static Key Y => new Key { KeyCode = 89, ScanCode = 21, Extended = false };

		/// <summary>
		/// Gets the z key.
		/// </summary>
		/// <value>
		/// The z key.
		/// </value>
		public static Key Z => new Key { KeyCode = 90, ScanCode = 44, Extended = false };

		/// <summary>
		/// Gets the d1 key.
		/// </summary>
		/// <value>
		/// The d1 key.
		/// </value>
		public static Key D1 => new Key { KeyCode = 49, ScanCode = 2, Extended = false };

		/// <summary>
		/// Gets the d2 key.
		/// </summary>
		/// <value>
		/// The d2 key.
		/// </value>
		public static Key D2 => new Key { KeyCode = 50, ScanCode = 3, Extended = false };

		/// <summary>
		/// Gets the d3 key.
		/// </summary>
		/// <value>
		/// The d3 key.
		/// </value>
		public static Key D3 => new Key { KeyCode = 51, ScanCode = 4, Extended = false };

		/// <summary>
		/// Gets the d4 key.
		/// </summary>
		/// <value>
		/// The d4 key.
		/// </value>
		public static Key D4 => new Key { KeyCode = 52, ScanCode = 5, Extended = false };

		/// <summary>
		/// Gets the d5 key.
		/// </summary>
		/// <value>
		/// The d5 key.
		/// </value>
		public static Key D5 => new Key { KeyCode = 53, ScanCode = 6, Extended = false };

		/// <summary>
		/// Gets the d6 key.
		/// </summary>
		/// <value>
		/// The d6 key.
		/// </value>
		public static Key D6 => new Key { KeyCode = 54, ScanCode = 7, Extended = false };

		/// <summary>
		/// Gets the d7 key.
		/// </summary>
		/// <value>
		/// The d7 key.
		/// </value>
		public static Key D7 => new Key { KeyCode = 55, ScanCode = 8, Extended = false };

		/// <summary>
		/// Gets the d8 key.
		/// </summary>
		/// <value>
		/// The d8 key.
		/// </value>
		public static Key D8 => new Key { KeyCode = 56, ScanCode = 9, Extended = false };

		/// <summary>
		/// Gets the d9 key.
		/// </summary>
		/// <value>
		/// The d9 key.
		/// </value>
		public static Key D9 => new Key { KeyCode = 57, ScanCode = 10, Extended = false };

		/// <summary>
		/// Gets the d0 key.
		/// </summary>
		/// <value>
		/// The d0 key.
		/// </value>
		public static Key D0 => new Key { KeyCode = 48, ScanCode = 11, Extended = false };

		/// <summary>
		/// Gets the escape key.
		/// </summary>
		/// <value>
		/// The escape key.
		/// </value>
		public static Key Escape => new Key { KeyCode = 27, ScanCode = 1, Extended = false };

		/// <summary>
		/// Gets the tab key.
		/// </summary>
		/// <value>
		/// The tab key.
		/// </value>
		public static Key Tab => new Key { KeyCode = 9, ScanCode = 15, Extended = false };

		/// <summary>
		/// Gets the f1 key.
		/// </summary>
		/// <value>
		/// The f1 key.
		/// </value>
		public static Key F1 => new Key { KeyCode = 112, ScanCode = 59, Extended = false };

		/// <summary>
		/// Gets the f2 key.
		/// </summary>
		/// <value>
		/// The f2 key.
		/// </value>
		public static Key F2 => new Key { KeyCode = 113, ScanCode = 60, Extended = false };

		/// <summary>
		/// Gets the f3 key.
		/// </summary>
		/// <value>
		/// The f3 key.
		/// </value>
		public static Key F3 => new Key { KeyCode = 114, ScanCode = 61, Extended = false };

		/// <summary>
		/// Gets the f4 key.
		/// </summary>
		/// <value>
		/// The f4 key.
		/// </value>
		public static Key F4 => new Key { KeyCode = 115, ScanCode = 62, Extended = false };

		/// <summary>
		/// Gets the f5 key.
		/// </summary>
		/// <value>
		/// The f5 key.
		/// </value>
		public static Key F5 => new Key { KeyCode = 116, ScanCode = 63, Extended = false };

		/// <summary>
		/// Gets the f6 key.
		/// </summary>
		/// <value>
		/// The f6 key.
		/// </value>
		public static Key F6 => new Key { KeyCode = 117, ScanCode = 64, Extended = false };

		/// <summary>
		/// Gets the f7 key.
		/// </summary>
		/// <value>
		/// The f7 key.
		/// </value>
		public static Key F7 => new Key { KeyCode = 118, ScanCode = 65, Extended = false };

		/// <summary>
		/// Gets the f8 key.
		/// </summary>
		/// <value>
		/// The f8 key.
		/// </value>
		public static Key F8 => new Key { KeyCode = 119, ScanCode = 66, Extended = false };

		/// <summary>
		/// Gets the f9 key.
		/// </summary>
		/// <value>
		/// The f9 key.
		/// </value>
		public static Key F9 => new Key { KeyCode = 120, ScanCode = 67, Extended = false };

		/// <summary>
		/// Gets the F10 key.
		/// </summary>
		/// <value>
		/// The F10 key.
		/// </value>
		public static Key F10 => new Key { KeyCode = 121, ScanCode = 68, Extended = false };

		/// <summary>
		/// Gets the F12 key.
		/// </summary>
		/// <value>
		/// The F12 key.
		/// </value>
		public static Key F12 => new Key { KeyCode = 123, ScanCode = 88, Extended = false };

		/// <summary>
		/// Gets the print screen key.
		/// </summary>
		/// <value>
		/// The print screen key.
		/// </value>
		public static Key PrintScreen => new Key { KeyCode = 44, ScanCode = 55, Extended = true };

		/// <summary>
		/// Gets the scroll lock key.
		/// </summary>
		/// <value>
		/// The scroll lock key.
		/// </value>
		public static Key ScrollLock => new Key { KeyCode = 145, ScanCode = 70, Extended = false };

		/// <summary>
		/// Gets the pause key.
		/// </summary>
		/// <value>
		/// The pause key.
		/// </value>
		public static Key Pause => new Key { KeyCode = 19, ScanCode = 69, Extended = false };

		/// <summary>
		/// Gets the extended nvda key key.
		/// </summary>
		/// <value>
		/// The extended nvda key.
		/// </value>
		public static Key ExtendedNvda => new Key { KeyCode = 45, ScanCode = 82, Extended = true };

		/// <summary>
		/// Gets the nvda key.
		/// </summary>
		/// <value>
		/// The nvda key.
		/// </value>
		public static Key Nvda => new Key { KeyCode = 45, ScanCode = 82, Extended = true };

		/// <summary>
		/// Gets the home key.
		/// </summary>
		/// <value>
		/// The home key.
		/// </value>
		public static Key Home => new Key { KeyCode = 36, ScanCode = 71, Extended = true };

		/// <summary>
		/// Gets the page up key.
		/// </summary>
		/// <value>
		/// The page up key.
		/// </value>
		public static Key PageUp => new Key { KeyCode = 33, ScanCode = 73, Extended = true };

		/// <summary>
		/// Gets the delete key.
		/// </summary>
		/// <value>
		/// The delete key.
		/// </value>
		public static Key Delete => new Key { KeyCode = 46, ScanCode = 83, Extended = true };

		/// <summary>
		/// Gets the end key.
		/// </summary>
		/// <value>
		/// The end key.
		/// </value>
		public static Key End => new Key { KeyCode = 35, ScanCode = 79, Extended = true };

		/// <summary>
		/// Gets the page down key.
		/// </summary>
		/// <value>
		/// The page down key.
		/// </value>
		public static Key PageDown => new Key { KeyCode = 34, ScanCode = 81, Extended = true };

		/// <summary>
		/// Gets the up arrow key.
		/// </summary>
		/// <value>
		/// Up arrow key.
		/// </value>
		public static Key UpArrow => new Key { KeyCode = 38, ScanCode = 72, Extended = true };

		/// <summary>
		/// Gets the down arrow key.
		/// </summary>
		/// <value>
		/// Down arrow key.
		/// </value>
		public static Key DownArrow => new Key { KeyCode = 40, ScanCode = 80, Extended = true };

		/// <summary>
		/// Gets the left arrow key.
		/// </summary>
		/// <value>
		/// The left arrow key.
		/// </value>
		public static Key LeftArrow => new Key { KeyCode = 37, ScanCode = 75, Extended = true };

		/// <summary>
		/// Gets the right arrow key.
		/// </summary>
		/// <value>
		/// The right arrow key.
		/// </value>
		public static Key RightArrow => new Key { KeyCode = 39, ScanCode = 77, Extended = true };

		/// <summary>
		/// Gets the number pad1 key.
		/// </summary>
		/// <value>
		/// The number pad1 key.
		/// </value>
		public static Key NumPad1 => new Key { KeyCode = 35, ScanCode = 79, Extended = false };

		/// <summary>
		/// Gets the number pad2 key.
		/// </summary>
		/// <value>
		/// The number pad2 key.
		/// </value>
		public static Key NumPad2 => new Key { KeyCode = 40, ScanCode = 80, Extended = false };

		/// <summary>
		/// Gets the number pad3 key.
		/// </summary>
		/// <value>
		/// The number pad3 key.
		/// </value>
		public static Key NumPad3 => new Key { KeyCode = 34, ScanCode = 81, Extended = false };

		/// <summary>
		/// Gets the number pad4 key.
		/// </summary>
		/// <value>
		/// The number pad4 key.
		/// </value>
		public static Key NumPad4 => new Key { KeyCode = 37, ScanCode = 75, Extended = false };

		/// <summary>
		/// Gets the number pad5 key.
		/// </summary>
		/// <value>
		/// The number pad5 key.
		/// </value>
		public static Key NumPad5 => new Key { KeyCode = 12, ScanCode = 76, Extended = false };

		/// <summary>
		/// Gets the number pad6 key.
		/// </summary>
		/// <value>
		/// The number pad6 key.
		/// </value>
		public static Key NumPad6 => new Key { KeyCode = 39, ScanCode = 77, Extended = false };

		/// <summary>
		/// Gets the number pad7 key.
		/// </summary>
		/// <value>
		/// The number pad7 key.
		/// </value>
		public static Key NumPad7 => new Key { KeyCode = 36, ScanCode = 71, Extended = false };

		/// <summary>
		/// Gets the number pad8 key.
		/// </summary>
		/// <value>
		/// The number pad8 key.
		/// </value>
		public static Key NumPad8 => new Key { KeyCode = 38, ScanCode = 72, Extended = false };

		/// <summary>
		/// Gets the number pad9 key.
		/// </summary>
		/// <value>
		/// The number pad9 key.
		/// </value>
		public static Key NumPad9 => new Key { KeyCode = 33, ScanCode = 73, Extended = false };

		/// <summary>
		/// Gets the number pad0 key.
		/// </summary>
		/// <value>
		/// The number pad0 key.
		/// </value>
		public static Key NumPad0 => new Key { KeyCode = 45, ScanCode = 82, Extended = false };

		/// <summary>
		/// Gets the oem1 key ([ in english, ` in spanish).
		/// </summary>
		/// <value>
		/// The oem1 key.
		/// </value>
		public static Key Oem1 => new Key { KeyCode = 186, ScanCode = 26, Extended = false };

		/// <summary>
		/// Gets the oem2 (\ in english, ç in spanish).
		/// </summary>
		/// <value>
		/// The oem2 key.
		/// </value>
		public static Key Oem2 => new Key { KeyCode = 191, ScanCode = 43, Extended = false };

		/// <summary>
		/// Gets the oem4 key (hiphen in english, apostrof in spanish...).
		/// </summary>
		/// <value>
		/// The oem4 key.
		/// </value>
		public static Key Oem4 => new Key { KeyCode = 219, ScanCode = 12, Extended = false };

		/// <summary>
		/// Gets the oem5 key (` in english, º in spanish).
		/// </summary>
		/// <value>
		/// The oem5 key.
		/// </value>
		public static Key Oem5 => new Key { KeyCode = 220, ScanCode = 41, Extended = false };

		/// <summary>
		/// Gets the oem6 key (= in english, ¡ in spanish).
		/// </summary>
		/// <value>
		/// The oem6 key.
		/// </value>
		public static Key Oem6 => new Key { KeyCode = 221, ScanCode = 13, Extended = false };

		/// <summary>
		/// Gets the oem7 key (' in english, ´ in spanish).
		/// </summary>
		/// <value>
		/// The oem7 key.
		/// </value>
		public static Key Oem7 => new Key { KeyCode = 222, ScanCode = 40, Extended = false };

		/// <summary>
		/// Gets the oem 102 (\ in english, \"menor que\" in spanish).
		/// </summary>
		/// <value>
		/// The oe M102 key.
		/// </value>
		public static Key OEM102 => new Key { KeyCode = 226, ScanCode = 86, Extended = false };

		/// <summary>
		/// Gets the oem coma key.
		/// </summary>
		/// <value>
		/// The oem coma key.
		/// </value>
		public static Key OEMComa => new Key { KeyCode = 188, ScanCode = 51, Extended = false };

		/// <summary>
		/// Gets the oem period key.
		/// </summary>
		/// <value>
		/// The oem period key.
		/// </value>
		public static Key OemPeriod => new Key { KeyCode = 190, ScanCode = 52, Extended = false };

		/// <summary>
		/// Gets the oem minus key.
		/// </summary>
		/// <value>
		/// The oem minus key.
		/// </value>
		public static Key OEMMinus => new Key { KeyCode = 189, ScanCode = 53, Extended = false };

		/// <summary>
		/// Gets the oem plus key (] in english, + in spanish).
		/// </summary>
		/// <value>
		/// The oem plus key.
		/// </value>
		public static Key OemPlus => new Key { KeyCode = 187, ScanCode = 27, Extended = false };

		/// <summary>
		/// Gets the backspace key.
		/// </summary>
		/// <value>
		/// The backspace key.
		/// </value>
		public static Key Backspace => new Key { KeyCode = 8, ScanCode = 14, Extended = false };

		/// <summary>
		/// Gets the number pad delete key.
		/// </summary>
		/// <value>
		/// The number pad delete key.
		/// </value>
		public static Key NumPadDelete => new Key { KeyCode = 46, ScanCode = 83, Extended = false };

		/// <summary>
		/// Gets the number pad enter key.
		/// </summary>
		/// <value>
		/// The number pad enter key.
		/// </value>
		public static Key NumPadEnter => new Key { KeyCode = 13, ScanCode = 28, Extended = true };

		/// <summary>
		/// Gets the enter key.
		/// </summary>
		/// <value>
		/// The enter key.
		/// </value>
		public static Key Enter => new Key { KeyCode = 13, ScanCode = 28, Extended = false };

		/// <summary>
		/// Gets the number pad plus key.
		/// </summary>
		/// <value>
		/// The number pad plus key.
		/// </value>
		public static Key NumPadPlus => new Key { KeyCode = 107, ScanCode = 78, Extended = false };

		/// <summary>
		/// Gets the number pad minus key.
		/// </summary>
		/// <value>
		/// The number pad minus key.
		/// </value>
		public static Key NumPadMinus => new Key { KeyCode = 109, ScanCode = 74, Extended = false };

		/// <summary>
		/// Gets the number pad multiply key.
		/// </summary>
		/// <value>
		/// The number pad multiply key.
		/// </value>
		public static Key NumPadMultiply => new Key { KeyCode = 106, ScanCode = 55, Extended = false };

		/// <summary>
		/// Gets the number pad divide key.
		/// </summary>
		/// <value>
		/// The number pad divide key.
		/// </value>
		public static Key NumPadDivide => new Key { KeyCode = 111, ScanCode = 53, Extended = true };

		/// <summary>
		/// Gets the shift key.
		/// </summary>
		/// <value>
		/// The shift key.
		/// </value>
		public static Key Shift => new Key { KeyCode = 160, ScanCode = 42, Extended = false };

		/// <summary>
		/// Gets the left control key.
		/// </summary>
		/// <value>
		/// The control key.
		/// </value>
		public static Key Control => new Key { KeyCode = 162, ScanCode = 29, Extended = false };

		/// <summary>
		/// Gets the windows key key.
		/// </summary>
		/// <value>
		/// The windows key key.
		/// </value>
		public static Key WindowsKey => new Key { KeyCode = 91, ScanCode = 91, Extended = true };

		/// <summary>
		/// Gets the left alt key.
		/// </summary>
		/// <value>
		/// The left alt key.
		/// </value>
		public static Key LeftAlt => new Key { KeyCode = 164, ScanCode = 56, Extended = false };

		/// <summary>
		/// Gets the space key.
		/// </summary>
		/// <value>
		/// The space key.
		/// </value>
		public static Key Space => new Key { KeyCode = 32, ScanCode = 57, Extended = false };

		/// <summary>
		/// Gets the right alt key.
		/// </summary>
		/// <value>
		/// The right alt key.
		/// </value>
		public static Key RightAlt => new Key { KeyCode = 162, ScanCode = 541, Extended = false };

		/// <summary>
		/// Gets the application key.
		/// </summary>
		/// <value>
		/// The application key.
		/// </value>
		public static Key Application => new Key { KeyCode = 93, ScanCode = 93, Extended = true };

		/// <summary>
		/// Gets the scan code.
		/// </summary>
		/// <value>
		/// The scan code.
		/// </value>
		public int ScanCode { get; private set; }

		/// <summary>
		/// Gets the key code.
		/// </summary>
		/// <value>
		/// The key code.
		/// </value>
		public int KeyCode { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this key is extended or not <see cref="Key"/> is extended.
		/// </summary>
		/// <value>
		///   <c>true</c> if extended; otherwise, <c>false</c>.
		/// </value>
		public bool Extended { get; private set; }
	}
}