// Copyright (C) 2020 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System;

using Microsoft.Extensions.Logging;

namespace NvdaTestingDriver.Settings
{
	/// <summary>
	/// Class to store all NVDA options which affect to NVDA behavior during testing
	/// </summary>
	public class NvdaDriverOptions
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaDriverOptions"/> class.
		/// </summary>
		public NvdaDriverOptions()
		{
			GeneralSettings = new GeneralSettings
			{
				Language = NvdaLanguage.English,
				PlayStartAndExitSounds = true,
			};

			SpeechSettings = new SpeechSettings
			{
				AutoDialectSwitching = false,
				AutoLanguageSwitching = true,
				IncludeUnicodeDescriptions = true,
				PunctuationLevel = PunctuationLevel.Some,
				SayCapForCapitals = false,
				UseSpellingFunctionality = true,
			};

			VisionSettings = new VisionSettings
			{
				HighlightBrowseMode = true,
				HighlightNavigator = true,
				HighlightFocus = true,
			};

			DocumentFormattingSettings = new DocumentFormattingSettings
			{
				ReportFontName = false,
				ReportFontSize = false,
				ReportFontAttributes = false,
				ReportEmphasis = false,
				ReportStyle = false,
				ReportColor = false,
				ReportComments = true,
				ReportRevisions = true,
				ReportSpellingErrors = true,
				ReportPage = true,
				ReportLineNumber = false,
				ReportLineIndentation = false,
				ReportParagraphIndentation = false,
				ReportLineSpacing = false,
				ReportAlignment = false,
				ReportTables = true,
				ReportTableHeaders = true,
				ReportTableCellCoords = true,
				ReportBorderColor = false,
				ReportBorderStyle = false,
				ReportHeadings = true,
				ReportLinks = true,
				ReportLists = true,
				ReportBlockQuotes = true,
				ReportLandmarks = true,
				ReportFrames = true,
				ReportClickable = true,
				DetectFormatAfterCursor = false,
				IncludeLayoutTables = false,
			};

			InputCompositionSettings = new InputCompositionSettings
			{
				AutoReportAllCandidates = true,
				AnnounceSelectedCandidate = true,
				AlwaysIncludeShortCharacterDescriptionInCandidateName = true,
				ReportReadingStringChanges = true,
				ReportCompositionStringChanges = true,
			};

			KeyboardSettings = new KeyboardSettings
			{
				KeyboardLayout = KeyboardLayout.Desktop,
				SpeakTypedCharacters = true,
				SpeakTypedWords = false,
				SpeechInterruptForEnter = true,
			};

			PresentationSettings = new PresentationSettings
			{
				ReportTooltips = false,
				ReportHelpBalloons = true,
				ReportKeyboardShortcuts = true,
				ReportObjectPositionInformation = true,
				GuessObjectPositionInformationWhenUnavailable = false,
				ReportObjectDescriptions = true,
				ProgressBarUpdates = new ProgressBarUpdateSettings
				{
					ProgressBarOutputMode = ProgressBarOutputModes.Beep,
					ReportBackgroundProgressBars = false,
				},
				ReportDynamicContentChanges = true,
			};

			ReviewCursorSettings = new ReviewCursorSettings
			{
				FollowFocus = true,
				FollowCaret = true,
				FollowMouse = false,
				SimpleReviewMode = true,
			};

			BrowseModeSettings = new BrowseModesSettings
			{
				MaxLineLength = 100,
				LinesPerPage = 25,
				UseScreenLayout = true,
				AutoSayAllOnPageLoad = true,
				AutoPassThroughOnCaretMove = false,
				AutoPassThroughOnFocusChange = true,
				TrapNonCommandGestures = true,
			};
			EnableLogging = false;
			DefaultTimeoutt = TimeSpan.FromSeconds(3);
			DefaultTimeoutWaitingForNewMessages = TimeSpan.FromMilliseconds(500);
		}

		/// <summary>
		/// Gets or sets the general settings.
		/// </summary>
		/// <value>
		/// The general settings.
		/// </value>
		public GeneralSettings GeneralSettings { get; set; }

		/// <summary>
		/// Gets or sets the speech settings.
		/// </summary>
		/// <value>
		/// The speech settings.
		/// </value>
		public SpeechSettings SpeechSettings { get; set; }

		/// <summary>
		/// Gets or sets the vision settings.
		/// </summary>
		/// <value>
		/// The vision settings.
		/// </value>
		public VisionSettings VisionSettings { get; set; }

		/// <summary>
		/// Gets or sets the document formatting settings.
		/// </summary>
		/// <value>
		/// The document formatting settings.
		/// </value>
		public DocumentFormattingSettings DocumentFormattingSettings { get; set; }

		/// <summary>
		/// Gets or sets the input composition settings.
		/// </summary>
		/// <value>
		/// The input composition settings.
		/// </value>
		public InputCompositionSettings InputCompositionSettings { get; set; }

		/// <summary>
		/// Gets or sets the keyboard settings.
		/// </summary>
		/// <value>
		/// The keyboard settings.
		/// </value>
		public KeyboardSettings KeyboardSettings { get; set; }

		/// <summary>
		/// Gets or sets the presentation settings.
		/// </summary>
		/// <value>
		/// The presentation settings.
		/// </value>
		public PresentationSettings PresentationSettings { get; set; }

		/// <summary>
		/// Gets or sets the review cursor settings.
		/// </summary>
		/// <value>
		/// The review cursor settings.
		/// </value>
		public ReviewCursorSettings ReviewCursorSettings { get; set; }

		/// <summary>
		/// Gets or sets the browse mode settings.
		/// </summary>
		/// <value>
		/// The browse mode settings.
		/// </value>
		public BrowseModesSettings BrowseModeSettings { get; set; }

		/// <summary>
		/// Gets or sets the logger factory.
		/// </summary>
		/// <value>
		/// The logger factory.
		/// </value>
		public ILoggerFactory LoggerFactory { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether logging should be enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if logging system should be enabled; otherwise, <c>false</c>.
		/// </value>
		public bool EnableLogging { get; set; }

		/// <summary>
		/// Gets or sets the default timeout for command execution.
		/// </summary>
		/// <value>
		/// The default command timeoutt.
		/// </value>
		public TimeSpan DefaultTimeoutt { get; set; }

		/// <summary>
		/// Gets or sets the default timeout to wait for new messages.
		/// There are occasions when NVDA transmits messages separately, in different packets.
		/// This field sets the default maximum time that NVDA will concatenate new messages received within the desired methods, starting from the arrival of the first message.
		/// </summary>
		/// <value>
		/// The default timeout to wait for new messages.
		/// </value>
		public TimeSpan DefaultTimeoutWaitingForNewMessages { get; set; }
 }
}
