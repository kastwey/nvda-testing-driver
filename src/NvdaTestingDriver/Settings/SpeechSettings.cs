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
	/// Sets the speech settings
	/// </summary>
	public class SpeechSettings
	{
		/// <summary>
		/// Gets or sets a value indicating whether NVDA should say the word &quot;cap&quot;
		/// before any capital letter when spoken as an individual character such as when spelling.
		/// </summary>
		/// <value>
		///   <c>true</c> if you want to instruct NVDA to say cap for capitals characters before pronounce them; otherwise, <c>false</c>.
		/// </value>
		public bool SayCapForCapitals { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should include unicode descriptions (useful for emojis).
		/// </summary>
		/// <value>
		///   <c>true</c> if you want to include unicode emojis  descriptions; otherwise, <c>false</c>.
		/// </value>
		public bool IncludeUnicodeDescriptions { get; set; }

		/// <summary>
		/// Gets or sets the punctuation level.
		/// </summary>
		/// <value>
		/// The punctuation level.
		/// </value>
		public PunctuationLevel PunctuationLevel { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether  use spelling functionality iss active
		/// Some words consist of only one character, but the pronunciation is different depending on whether the character is being spoken as an individual character
		/// (such as when spelling) or a word.For example,
		/// in English, "a" is both a letter and a word and is pronounced differently
		/// in each case. This option allows the synthesizer to differentiate between these
		/// two cases if the synthesizer supports this.
		/// Most synthesizers do support it.
		/// This option should generally be enabled. However, some Microsoft Speech API synthesizers do not
		/// implement this correctly and behave strangely when it is enabled.
		/// If you are having problems with the pronunciation of individual characters, try disabling this option.
		/// </summary>
		/// <value>
		///   <c>true</c> if [use spelling functionality]; otherwise, <c>false</c>.
		/// </value>
		public bool UseSpellingFunctionality { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should switch speech synthesizer languages
		/// automatically if the text being read specifies its language.
		/// This option is enabled by default. Currently only the eSpeak NG synthesizer supports automatic language switching.
		/// </summary>
		/// <value>
		///   <c>true</c> if  you want to enable automatic language switching; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>Currently this settings will have no effect on returned text, but will be implemented in the future.</remarks>
		public bool AutoLanguageSwitching { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not dialect changes should be made,
		/// rather than just actual language changes. For example, if reading in an English U.S.voice
		/// but a document specifies that some text is in English U.K., then the synthesizer will switch accents if this
		/// option is enabled. This option is disabled by default.
		/// </summary>
		/// <value>
		///   <c>true</c> if you want to enable the automatic dialect switching; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>Currently this settings will have no effect on returned text, but will be implemented in the future.</remarks>
		public bool AutoDialectSwitching { get; set; }
	}
}