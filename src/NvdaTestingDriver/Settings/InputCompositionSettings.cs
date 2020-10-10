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
	/// Sets the input composition settings.
	/// these settings allow you to control how NVDA reports the input of Asian characters,
	/// such as with IME or Text Service input methods.
	/// </summary>
	/// <remarks>Note that due to the fact that input methods vary greatly by available features and by how they convey information,
	/// it will most likely be necessary to configure these options differently for each input method
	/// to get the most efficient typing experience.</remarks>
	public class InputCompositionSettings
	{
		/// <summary>
		/// This option, which is on by default, allows you to choose whether or not
		/// all visible candidates should be reported automatically when a candidate list appears or its page is changed.
		/// Having this option on for pictographic input methods such as Chinese New ChangJie or Boshiami is useful,
		/// as you can automatically hear all symbols and their numbers and you can choose one right away.
		/// However, for phonetic input methods such as Chinese New Phonetic, it may be more useful to turn this option off,
		/// as all the symbols will sound the same and you will have to use the arrow keys
		/// to navigate the list items individually to gain more information from the character descriptions for each candidate.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should automatic report all candidates; otherwise, <c>false</c>.
		/// </value>
		public bool AutoReportAllCandidates { get; set; }

		/// <summary>
		/// This option, which is on by default, allows you to choose whether NVDA should announce the
		/// selected candidate when a candidate list appears or when the selection is changed.
		/// For input methods where the selection can be changed with the arrow keys
		/// (such as Chinese New Phonetic) this is necessary, but for some input methods
		/// it may be more efficient typing with this option turned off.
		/// Note that even with this option off, the review cursor will still be placed on the selected candidate
		/// allowing you to use object navigation / review to manually read this or other candidates.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce selected candidate; otherwise, <c>false</c>.
		/// </value>
		public bool AnnounceSelectedCandidate { get; set; }

		/// <summary>
		/// This option, which is on by default, allows you to choose whether or not NVDA should provide
		/// a short description for each character in a candidate, either when it's selected or when it's
		/// automatically read when the candidate list appears.
		/// Note that for locales such as Chinese, the announcement of extra
		/// character descriptions for the selected candidate is not affected by this option.
		/// This option may be useful for Korean and Japanese input methods.
		/// </summary>
		/// <value>
		///   <c>true</c> if  NVDA always should include short character description in candidate name; otherwise, <c>false</c>.
		/// </value>
		public bool AlwaysIncludeShortCharacterDescriptionInCandidateName { get; set; }

		/// <summary>
		/// Some input methods such as Chinese New Phonetic and New ChangJie have a reading string
		/// (sometimes known as a precomposition string). You can choose whether or not NVDA
		/// should announce new characters being typed into this reading string with this option.
		/// This option is on by default. Note: some older input methods such as Chinese ChangJie
		/// may not use the reading string to hold precomposition characters, but instead use the composition string directly.
		/// Please see the <seealso cref="ReportCompositionStringChanges "/> for configuring reporting of the composition string.
		/// </summary>
		/// <value>
		///   <c>true</c> if  NVDA should report reading string changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportReadingStringChanges { get; set; }

		/// <summary>
		/// After reading or precomposition data has been combined into a valid pictographic symbol,
		/// most input methods place this symbol into a composition string for temporary storage
		/// along with other combined symbols before they are finally inserted into the document.
		/// This option allows you to choose whether or not NVDA should report new symbols as they appear
		/// in the composition string. This option is on by default.
		/// </summary>
		/// <value>
		///   <c>true</c> if  NVDA should report composition string changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportCompositionStringChanges { get; set; }
	}
}
