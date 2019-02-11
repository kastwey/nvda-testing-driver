// Copyright (C) 2019 Juan José Montiel
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
	/// Sets the document formating settings
	/// </summary>
	public class DocumentFormattingSettings
	{
		/// <summary>
		/// This option affects how NVDA handles tables used purely for layout purposes.
		/// When on, NVDA will treat these as normal tables, reporting them based on Document Formatting Settings and locating them with quick navigation commands.
		/// When off, they will not be reported nor found with quick navigation.
		/// However, the content of the tables will still be included as normal text.
		/// This option is turned off by default.
		/// </summary>
		/// <value>
		///   <c>true</c> if [include layout tables; otherwise, <c>false</c>.
		/// </value>
		public bool IncludeLayoutTables { get; set; }

		/// <summary>
		/// If enabled, this setting tells NVDA to try and detect all the formatting changes
		/// on a line as it reports it, even if doing this may slow down NVDA's performance.
		/// By default, NVDA will detect the formatting at the position of the System caret / Review Cursor,
		/// and in some instances may detect formatting on the rest of the line,
		/// only if it is not going to cause a performance decrease.
		/// Enable this option while proof reading documents in applications such as WordPad, where formatting is important.
		/// </summary>
		/// <value>
		///   <c>true</c> if  NVDA should detect format after cursor; otherwise, <c>false</c>.
		/// </value>
		public bool DetectFormatAfterCursor { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether each time you arrow onto text with a different font, the name of the font should be announced.
		/// </summary>
		/// <value>
		///   <c>true</c> if you want NVDA to announce the name of the font when it changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportFontName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether each time you arrow onto text with a different font size, the size of the font should be announced.
		/// </summary>
		/// <value>
		///   <c>true</c> if you want NVDA to announce the size of the font when it changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportFontSize { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether each time you arrow onto text with a different font attributes, the new attributes of the font should be announced.
		/// </summary>
		/// <value>
		///   <c>true</c> if you want NVDA to announce the attributes of the font when them changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportFontAttributes { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether each time you arrow onto text with a different font emphasis, the new emphasis of the font should be announced.
		/// </summary>
		/// <value>
		///   <c>true</c> if you want NVDA to announce the emphasis of the font when it changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportEmphasis { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether each time you arrow onto text with a different font style, the new style of the font should be announced.
		/// </summary>
		/// <value>
		///   <c>true</c> if you want NVDA to announce the style of the font when it changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportStyle { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether each time you arrow onto text with a different font color, the new color of the font should be announced.
		/// </summary>
		/// <value>
		///   <c>true</c> if you want NVDA to announce the color of the font when it changes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportColor { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should read the comments in a document.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce document comments; otherwise, <c>false</c>.
		/// </value>
		public bool ReportComments { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce the editor revisions into a document.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce editor revisions; otherwise, <c>false</c>.
		/// </value>
		public bool ReportRevisions { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce spelling errors.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce spelling errors; otherwise, <c>false</c>.
		/// </value>
		public bool ReportSpellingErrors { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce line aligment.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce line aligment; otherwise, <c>false</c>.
		/// </value>
		public bool ReportAlignment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce page numbers.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce page numbers; otherwise, <c>false</c>.
		/// </value>
		public bool ReportPage { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce line numbers.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce line numbers; otherwise, <c>false</c>.
		/// </value>
		public bool ReportLineNumber { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce line indentation by voice (text, in this case).
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce by voice (text in this case) line indentation; otherwise, <c>false</c>.
		/// </value>
		public bool ReportLineIndentation { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce paragraph indentation.
		/// </summary>
		/// <value>
		///   <c>true</c> if  NVDA should announce paragraph indentation; otherwise, <c>false</c>.
		/// </value>
		public bool ReportParagraphIndentation { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether  NVDA should announce line spacing.
		/// </summary>
		/// <value>
		///   <c>true</c> if  NVDA should announce line spacing; otherwise, <c>false</c>.
		/// </value>
		public bool ReportLineSpacing { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether  NVDA should announce tables.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce tables; otherwise, <c>false</c>.
		/// </value>
		public bool ReportTables { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce table headers.
		/// </summary>
		/// <value>
		///   <c>true</c> if  NVDA should announce table headers; otherwise, <c>false</c>.
		/// </value>
		public bool ReportTableHeaders { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce table cell coordinates.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce table cell coordinatess; otherwise, <c>false</c>.
		/// </value>
		public bool ReportTableCellCoords { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce cell borders style.
		/// </summary>
		/// <value>
		///   <c>true</c> if NDA should announce cell borders style; otherwise, <c>false</c>.
		/// </value>
		public bool ReportBorderStyle { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce cell borders color.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce cell borders color; otherwise, <c>false</c>.
		/// </value>
		public bool ReportBorderColor { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should report document links.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce document links; otherwise, <c>false</c>.
		/// </value>
		public bool ReportLinks { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce document headings.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce document headings; otherwise, <c>false</c>.
		/// </value>
		public bool ReportHeadings { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce document lists.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce document lists; otherwise, <c>false</c>.
		/// </value>
		public bool ReportLists { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce document block quotes.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce document block quotes; otherwise, <c>false</c>.
		/// </value>
		public bool ReportBlockQuotes { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce document landmarks.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce document landmarks; otherwise, <c>false</c>.
		/// </value>
		public bool ReportLandmarks { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce document frames.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce document frames; otherwise, <c>false</c>.
		/// </value>
		public bool ReportFrames { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether NVDA should announce document clickable elements.
		/// </summary>
		/// <value>
		///   <c>true</c> if NVDA should announce document clickable elements; otherwise, <c>false</c>.
		/// </value>
		public bool ReportClickable { get; set; }
	}
}
