// Copyright (C) 2020 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System.IO;

using System.Text;

using NvdaTestingDriver.Extensions;

namespace NvdaTestingDriver.Settings
{
	/// <summary>
	/// Class to write NvdaOptions into nvda.ini file
	/// </summary>
	internal class NvdaDriverOptionsWriter
	{
		private readonly NvdaDriverOptions _nvdaDriverOptions;

		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaDriverOptionsWriter"/> class.
		/// </summary>
		/// <param name="nvdaDriverOptions">The nvda driver options.</param>
		internal NvdaDriverOptionsWriter(NvdaDriverOptions nvdaDriverOptions)
		{
			_nvdaDriverOptions = nvdaDriverOptions;
		}

		/// <summary>
		/// Writes the options to ini file.
		/// </summary>
		/// <param name="iniFilePath">The ini file path.</param>
		internal void WriteOptionsToIniFile(string iniFilePath)
		{
			var progressBarOutputModeOpt = _nvdaDriverOptions.PresentationSettings.ProgressBarUpdates.ProgressBarOutputMode;
			var progressBarOutputModeStr = (progressBarOutputModeOpt & ProgressBarOutputModes.Beep) == ProgressBarOutputModes.Beep
				&& (progressBarOutputModeOpt & ProgressBarOutputModes.Speak) == ProgressBarOutputModes.Speak ?
				"both" : progressBarOutputModeOpt.ToString().ToLowerInvariant();
			var iniFileContent = $@"schemaVersion = 3
[development]
[upgrade]
[update]
	allowUsageStats = False
	askedAllowUsageStats = True
	autoCheck = False
	startupNotification = False
[general]
	showWelcomeDialogAtStartup = False
	language = {_nvdaDriverOptions.GeneralSettings.Language.Value}
	saveConfigurationOnExit = False
	askToExit = False
	playStartAndExitSounds = {_nvdaDriverOptions.GeneralSettings.PlayStartAndExitSounds.ToFirstCapitalizedString()}
	loggingLevel = DEBUG
[speech]
	synth = oneCore
	autoLanguageSwitching = {_nvdaDriverOptions.SpeechSettings.AutoLanguageSwitching.ToFirstCapitalizedString()}
	autoDialectSwitching = {_nvdaDriverOptions.SpeechSettings.AutoDialectSwitching.ToFirstCapitalizedString()}
	trustVoiceLanguage = False
	includeCLDR = {_nvdaDriverOptions.SpeechSettings.IncludeUnicodeDescriptions.ToFirstCapitalizedString()}
	symbolLevel = {GetSymbolLevel()}
	[[silence]]
		sayCapForCapitals = {_nvdaDriverOptions.SpeechSettings.SayCapForCapitals.ToFirstCapitalizedString()}
[braille]
	[[noBraille]]
		port = """"
[vision]
	[[NVDAHighlighter]]
		highlightFocus = {_nvdaDriverOptions.VisionSettings.HighlightFocus.ToFirstCapitalizedString()}
		highlightNavigator = {_nvdaDriverOptions.VisionSettings.HighlightNavigator.ToFirstCapitalizedString()}
		highlightBrowseMode = {_nvdaDriverOptions.VisionSettings.HighlightBrowseMode.ToFirstCapitalizedString()}
	[[screenCurtain]]
[keyboard]
	keyboardLayout = {(_nvdaDriverOptions.KeyboardSettings.KeyboardLayout == KeyboardLayout.Desktop ? "desktop" : "laptop")}
	useCapsLockAsNVDAModifierKey = {_nvdaDriverOptions.KeyboardSettings.UseCapsLockAsNVDAModifierKey.ToFirstCapitalizedString()}
	speakTypedCharacters = {_nvdaDriverOptions.KeyboardSettings.SpeakTypedCharacters.ToFirstCapitalizedString()}
	speakTypedWords = {_nvdaDriverOptions.KeyboardSettings.SpeakTypedWords.ToFirstCapitalizedString()}
	speechInterruptForEnter = {_nvdaDriverOptions.KeyboardSettings.SpeechInterruptForEnter.ToFirstCapitalizedString()}
[reviewCursor]
	followFocus = {_nvdaDriverOptions.ReviewCursorSettings.FollowFocus.ToFirstCapitalizedString()}
	followCaret = {_nvdaDriverOptions.ReviewCursorSettings.FollowCaret.ToFirstCapitalizedString()}
	followMouse = {_nvdaDriverOptions.ReviewCursorSettings.FollowMouse.ToFirstCapitalizedString()}
	simpleReviewMode = {_nvdaDriverOptions.ReviewCursorSettings.SimpleReviewMode.ToFirstCapitalizedString()}
[inputComposition]
	autoReportAllCandidates = {_nvdaDriverOptions.InputCompositionSettings.AutoReportAllCandidates.ToFirstCapitalizedString()}
	announceSelectedCandidate = {_nvdaDriverOptions.InputCompositionSettings.AnnounceSelectedCandidate.ToFirstCapitalizedString()}
	alwaysIncludeShortCharacterDescriptionInCandidateName = {_nvdaDriverOptions.InputCompositionSettings.AlwaysIncludeShortCharacterDescriptionInCandidateName.ToFirstCapitalizedString()}
	reportReadingStringChanges = {_nvdaDriverOptions.InputCompositionSettings.ReportReadingStringChanges.ToFirstCapitalizedString()}
	reportCompositionStringChanges = {_nvdaDriverOptions.InputCompositionSettings.ReportCompositionStringChanges.ToFirstCapitalizedString()}
[presentation]
	reportTooltips = {_nvdaDriverOptions.PresentationSettings.ReportTooltips.ToFirstCapitalizedString()}
	reportHelpBalloons = {_nvdaDriverOptions.PresentationSettings.ReportHelpBalloons.ToFirstCapitalizedString()}
	reportKeyboardShortcuts = {_nvdaDriverOptions.PresentationSettings.ReportKeyboardShortcuts.ToFirstCapitalizedString()}
	reportObjectPositionInformation = {_nvdaDriverOptions.PresentationSettings.ReportObjectPositionInformation.ToFirstCapitalizedString()}
	guessObjectPositionInformationWhenUnavailable = {_nvdaDriverOptions.PresentationSettings.GuessObjectPositionInformationWhenUnavailable.ToFirstCapitalizedString()}
	reportObjectDescriptions = {_nvdaDriverOptions.PresentationSettings.ReportObjectDescriptions.ToFirstCapitalizedString()}
	reportDynamicContentChanges = {_nvdaDriverOptions.PresentationSettings.ReportDynamicContentChanges.ToFirstCapitalizedString()}
	reportAutoSuggestionsWithSound = False
	[[progressBarUpdates]]
		progressBarOutputMode = {progressBarOutputModeStr}
		reportBackgroundProgressBars = {_nvdaDriverOptions.PresentationSettings.ProgressBarUpdates.ReportBackgroundProgressBars.ToFirstCapitalizedString()}
[virtualBuffers]
	maxLineLength = {_nvdaDriverOptions.BrowseModeSettings.MaxLineLength}
	linesPerPage = {_nvdaDriverOptions.BrowseModeSettings.LinesPerPage}
	useScreenLayout = {_nvdaDriverOptions.BrowseModeSettings.UseScreenLayout.ToFirstCapitalizedString()}
	autoSayAllOnPageLoad = {_nvdaDriverOptions.BrowseModeSettings.AutoSayAllOnPageLoad.ToFirstCapitalizedString()}
	autoPassThroughOnFocusChange = {_nvdaDriverOptions.BrowseModeSettings.AutoPassThroughOnFocusChange.ToFirstCapitalizedString()}
	autoPassThroughOnCaretMove = {_nvdaDriverOptions.BrowseModeSettings.AutoPassThroughOnCaretMove.ToFirstCapitalizedString()}
	passThroughAudioIndication = False
	trapNonCommandGestures = {_nvdaDriverOptions.BrowseModeSettings.TrapNonCommandGestures.ToFirstCapitalizedString()}
[documentFormatting]
	includeLayoutTables = {_nvdaDriverOptions.DocumentFormattingSettings.IncludeLayoutTables.ToFirstCapitalizedString()}
	detectFormatAfterCursor = {_nvdaDriverOptions.DocumentFormattingSettings.DetectFormatAfterCursor.ToFirstCapitalizedString()}
	reportFontName = {_nvdaDriverOptions.DocumentFormattingSettings.ReportFontName.ToFirstCapitalizedString()}
	reportFontSize = {_nvdaDriverOptions.DocumentFormattingSettings.ReportFontSize.ToFirstCapitalizedString()}
	reportFontAttributes = {_nvdaDriverOptions.DocumentFormattingSettings.ReportFontAttributes.ToFirstCapitalizedString()}
	reportColor = {_nvdaDriverOptions.DocumentFormattingSettings.ReportColor.ToFirstCapitalizedString()}
	reportComments = {_nvdaDriverOptions.DocumentFormattingSettings.ReportComments.ToFirstCapitalizedString()}
	reportRevisions = {_nvdaDriverOptions.DocumentFormattingSettings.ReportRevisions.ToFirstCapitalizedString()}
	reportEmphasis = {_nvdaDriverOptions.DocumentFormattingSettings.ReportEmphasis.ToFirstCapitalizedString()}
	reportAlignment = {_nvdaDriverOptions.DocumentFormattingSettings.ReportAlignment.ToFirstCapitalizedString()}
	reportStyle = {_nvdaDriverOptions.DocumentFormattingSettings.ReportStyle.ToFirstCapitalizedString()}
	reportSpellingErrors = {_nvdaDriverOptions.DocumentFormattingSettings.ReportSpellingErrors.ToFirstCapitalizedString()}
	reportPage = {_nvdaDriverOptions.DocumentFormattingSettings.ReportPage.ToFirstCapitalizedString()}
	reportLineNumber = {_nvdaDriverOptions.DocumentFormattingSettings.ReportLineNumber.ToFirstCapitalizedString()}
	reportLineIndentation = {_nvdaDriverOptions.DocumentFormattingSettings.ReportLineIndentation.ToFirstCapitalizedString()}
	reportParagraphIndentation = {_nvdaDriverOptions.DocumentFormattingSettings.ReportParagraphIndentation.ToFirstCapitalizedString()}
	reportLineSpacing = {_nvdaDriverOptions.DocumentFormattingSettings.ReportLineSpacing.ToFirstCapitalizedString()}
	reportTables = {_nvdaDriverOptions.DocumentFormattingSettings.ReportTables.ToFirstCapitalizedString()}
	reportTableHeaders = {_nvdaDriverOptions.DocumentFormattingSettings.ReportTableHeaders.ToFirstCapitalizedString()}
	reportTableCellCoords = {_nvdaDriverOptions.DocumentFormattingSettings.ReportTableCellCoords.ToFirstCapitalizedString()}
	reportBorderStyle = {_nvdaDriverOptions.DocumentFormattingSettings.ReportBorderStyle.ToFirstCapitalizedString()}
	reportBorderColor = {_nvdaDriverOptions.DocumentFormattingSettings.ReportBorderColor.ToFirstCapitalizedString()}
	reportLinks = {_nvdaDriverOptions.DocumentFormattingSettings.ReportLinks.ToFirstCapitalizedString()}
	reportHeadings = {_nvdaDriverOptions.DocumentFormattingSettings.ReportHeadings.ToFirstCapitalizedString()}
	reportLists = {_nvdaDriverOptions.DocumentFormattingSettings.ReportLists.ToFirstCapitalizedString()}
	reportBlockQuotes = {_nvdaDriverOptions.DocumentFormattingSettings.ReportBlockQuotes.ToFirstCapitalizedString()}
	reportLandmarks = {_nvdaDriverOptions.DocumentFormattingSettings.ReportLandmarks.ToFirstCapitalizedString()}
	reportFrames = {_nvdaDriverOptions.DocumentFormattingSettings.ReportFrames.ToFirstCapitalizedString()}
	reportClickable = {_nvdaDriverOptions.DocumentFormattingSettings.ReportClickable.ToFirstCapitalizedString()}";
			File.WriteAllText(iniFilePath, iniFileContent, Encoding.ASCII);
		}

		/// <summary>
		/// Gets the punctuation level string to store it into ini value.
		/// </summary>
		/// <returns>The int representing the symbol level</returns>
		private int GetSymbolLevel()
		{
			switch (_nvdaDriverOptions.SpeechSettings.PunctuationLevel)
			{
				case PunctuationLevel.All: return 300;
				case PunctuationLevel.Most: return 200;
				case PunctuationLevel.Some: return 100;
				case PunctuationLevel.None: default: return 0;
			}
		}
	}
}
