// Copyright (C) 2019 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System;

namespace NvdaTestingDriver.Settings
{
	/// <summary>
	/// Struct to store the NVDA language.
	/// </summary>
	public struct NvdaLanguage : IEquatable<NvdaLanguage>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaLanguage"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		private NvdaLanguage(string value) => this.Value = value;

		/// <summary>
		/// Gets the arabic language.
		/// </summary>
		/// <value>
		/// The arabic language.
		/// </value>
		public static NvdaLanguage Arabic => new NvdaLanguage("ar");

		/// <summary>
		/// Gets the vietnamese language.
		/// </summary>
		/// <value>
		/// The vietnamese language.
		/// </value>
		public static NvdaLanguage Vietnamese => new NvdaLanguage("vi");

		/// <summary>
		/// Gets the urdu language.
		/// </summary>
		/// <value>
		/// The urdu language.
		/// </value>
		public static NvdaLanguage Urdu => new NvdaLanguage("ur");

		/// <summary>
		/// Gets the ukrainian language.
		/// </summary>
		/// <value>
		/// The ukrainian language.
		/// </value>
		public static NvdaLanguage Ukrainian => new NvdaLanguage("uk");

		/// <summary>
		/// Gets the turkish language.
		/// </summary>
		/// <value>
		/// The turkish language.
		/// </value>
		public static NvdaLanguage Turkish => new NvdaLanguage("tr");

		/// <summary>
		/// Gets the tamil language.
		/// </summary>
		/// <value>
		/// The tamil language.
		/// </value>
		public static NvdaLanguage Tamil => new NvdaLanguage("ta");

		/// <summary>
		/// Gets the thai language.
		/// </summary>
		/// <value>
		/// The thai language.
		/// </value>
		public static NvdaLanguage Thai => new NvdaLanguage("th");

		/// <summary>
		/// Gets the swedish language.
		/// </summary>
		/// <value>
		/// The swedish language.
		/// </value>
		public static NvdaLanguage Swedish => new NvdaLanguage("sv");

		/// <summary>
		/// Gets the somali language.
		/// </summary>
		/// <value>
		/// The somali language.
		/// </value>
		public static NvdaLanguage Somali => new NvdaLanguage("so");

		/// <summary>
		/// Gets the serbian latin language.
		/// </summary>
		/// <value>
		/// The serbian latin language.
		/// </value>
		public static NvdaLanguage SerbianLatin => new NvdaLanguage("sr");

		/// <summary>
		/// Gets the spanish languae.
		/// </summary>
		/// <value>
		/// The spanish language.
		/// </value>
		public static NvdaLanguage Spanish => new NvdaLanguage("es");

		/// <summary>
		/// Gets the punjabi language.
		/// </summary>
		/// <value>
		/// The punjabi language.
		/// </value>
		public static NvdaLanguage Punjabi => new NvdaLanguage("pa");

		/// <summary>
		/// Gets the polish language.
		/// </summary>
		/// <value>
		/// The polish language.
		/// </value>
		public static NvdaLanguage Polish => new NvdaLanguage("pl");

		/// <summary>
		/// Gets the norwegian bokmal norway language.
		/// </summary>
		/// <value>
		/// The norwegian bokmal norway language.
		/// </value>
		public static NvdaLanguage NorwegianBokmalNorway => new NvdaLanguage("nb_NO");

		/// <summary>
		/// Gets the dutch language.
		/// </summary>
		/// <value>
		/// The dutch language.
		/// </value>
		public static NvdaLanguage Dutch => new NvdaLanguage("nl");

		/// <summary>
		/// Gets the mongolian language.
		/// </summary>
		/// <value>
		/// The mongolian language.
		/// </value>
		public static NvdaLanguage Mongolian => new NvdaLanguage("mn");

		/// <summary>
		/// Gets the macedonian language.
		/// </summary>
		/// <value>
		/// The macedonian language.
		/// </value>
		public static NvdaLanguage Macedonian => new NvdaLanguage("mk");

		/// <summary>
		/// Gets the lithuanian language.
		/// </summary>
		/// <value>
		/// The lithuanian language.
		/// </value>
		public static NvdaLanguage Lithuanian => new NvdaLanguage("lt");

		/// <summary>
		/// Gets the central kurdish language.
		/// </summary>
		/// <value>
		/// The central kurdish language.
		/// </value>
		public static NvdaLanguage CentralKurdish => new NvdaLanguage("ckb");

		/// <summary>
		/// Gets the kyrgyz language.
		/// </summary>
		/// <value>
		/// The kyrgyz language.
		/// </value>
		public static NvdaLanguage Kyrgyz => new NvdaLanguage("ky");

		/// <summary>
		/// Gets the kannada language.
		/// </summary>
		/// <value>
		/// The kannada language.
		/// </value>
		public static NvdaLanguage Kannada => new NvdaLanguage("kn");

		/// <summary>
		/// Gets the japanese language.
		/// </summary>
		/// <value>
		/// The japanese language.
		/// </value>
		public static NvdaLanguage Japanese => new NvdaLanguage("ja");

		/// <summary>
		/// Gets the italian language.
		/// </summary>
		/// <value>
		/// The italian language.
		/// </value>
		public static NvdaLanguage Italian => new NvdaLanguage("it");

		/// <summary>
		/// Gets the icelandic language.
		/// </summary>
		/// <value>
		/// The icelandic language.
		/// </value>
		public static NvdaLanguage Icelandic => new NvdaLanguage("is");

		/// <summary>
		/// Gets the irish language.
		/// </summary>
		/// <value>
		/// The irish language.
		/// </value>
		public static NvdaLanguage Irish => new NvdaLanguage("ga");

		/// <summary>
		/// Gets the english language.
		/// </summary>
		/// <value>
		/// The english language.
		/// </value>
		public static NvdaLanguage English => new NvdaLanguage("en");

		/// <summary>
		/// Gets the indonesian language.
		/// </summary>
		/// <value>
		/// The indonesian language.
		/// </value>
		public static NvdaLanguage Indonesian => new NvdaLanguage("id");

		/// <summary>
		/// Gets the hungarian language.
		/// </summary>
		/// <value>
		/// The hungarian language.
		/// </value>
		public static NvdaLanguage Hungarian => new NvdaLanguage("hu");

		/// <summary>
		/// Gets the hindi language.
		/// </summary>
		/// <value>
		/// The hindi language.
		/// </value>
		public static NvdaLanguage Hindi => new NvdaLanguage("hi");

		/// <summary>
		/// Gets the hebrew language.
		/// </summary>
		/// <value>
		/// The hebrew language.
		/// </value>
		public static NvdaLanguage Hebrew => new NvdaLanguage("he");

		/// <summary>
		/// Gets the greek language.
		/// </summary>
		/// <value>
		/// The greek language.
		/// </value>
		public static NvdaLanguage Greek => new NvdaLanguage("the");

		/// <summary>
		/// Gets the georgiano language.
		/// </summary>
		/// <value>
		/// The georgiano language.
		/// </value>
		public static NvdaLanguage Georgiano => new NvdaLanguage("ka");

		/// <summary>
		/// Gets the galician language.
		/// </summary>
		/// <value>
		/// The galician language.
		/// </value>
		public static NvdaLanguage Galician => new NvdaLanguage("gl");

		/// <summary>
		/// Gets the finnish language.
		/// </summary>
		/// <value>
		/// The finnish language.
		/// </value>
		public static NvdaLanguage Finnish => new NvdaLanguage("fi");

		/// <summary>
		/// Gets the slovenian language.
		/// </summary>
		/// <value>
		/// The slovenian language.
		/// </value>
		public static NvdaLanguage Slovenian => new NvdaLanguage("sl");

		/// <summary>
		/// Gets the croatian language.
		/// </summary>
		/// <value>
		/// The croatian language.
		/// </value>
		public static NvdaLanguage Croatian => new NvdaLanguage("hr");

		/// <summary>
		/// Gets the korean language.
		/// </summary>
		/// <value>
		/// The korean language.
		/// </value>
		public static NvdaLanguage Korean => new NvdaLanguage("ko");

		/// <summary>
		/// Gets the chinese traditional hong kong language.
		/// </summary>
		/// <value>
		/// The chinese traditional hong kong language.
		/// </value>
		public static NvdaLanguage ChineseTraditionalHongKong => new NvdaLanguage("zh_HK");

		/// <summary>
		/// Gets the chinese traditional taiwan language.
		/// </summary>
		/// <value>
		/// The chinese traditional taiwan language.
		/// </value>
		public static NvdaLanguage ChineseTraditionalTaiwan => new NvdaLanguage("zh_TW");

		/// <summary>
		/// Gets the chinese simplified china language.
		/// </summary>
		/// <value>
		/// The chinese simplified china language.
		/// </value>
		public static NvdaLanguage ChineseSimplifiedChina => new NvdaLanguage("zh_CN");

		/// <summary>
		/// Gets the czech language.
		/// </summary>
		/// <value>
		/// The czech language.
		/// </value>
		public static NvdaLanguage Czech => new NvdaLanguage("cs");

		/// <summary>
		/// Gets the catalan language.
		/// </summary>
		/// <value>
		/// The catalan language.
		/// </value>
		public static NvdaLanguage Catalan => new NvdaLanguage("ca");

		/// <summary>
		/// Gets the bulgarian language.
		/// </summary>
		/// <value>
		/// The bulgarian language.
		/// </value>
		public static NvdaLanguage Bulgarian => new NvdaLanguage("bg");

		/// <summary>
		/// Gets the aragonese language.
		/// </summary>
		/// <value>
		/// The aragonese language.
		/// </value>
		public static NvdaLanguage Aragonese => new NvdaLanguage("an");

		/// <summary>
		/// Gets the german language.
		/// </summary>
		/// <value>
		/// The german language.
		/// </value>
		public static NvdaLanguage German => new NvdaLanguage("de");

		/// <summary>
		/// Gets the albanian language.
		/// </summary>
		/// <value>
		/// The albanian language.
		/// </value>
		public static NvdaLanguage Albanian => new NvdaLanguage("sq");

		/// <summary>
		/// Gets the afrikaans south africa language.
		/// </summary>
		/// <value>
		/// The afrikaans south africa language.
		/// </value>
		public static NvdaLanguage AfrikaansSouthAfrica => new NvdaLanguage("af_ZZ");

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value, which contains the Nvda language identifier.
		/// </value>
		internal string Value { get; set; }

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(NvdaLanguage left, NvdaLanguage right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(NvdaLanguage left, NvdaLanguage right)
		{
			return !(left == right);
		}

		/// <summary>
		/// Determines whether the specified <see cref="object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (!(obj is NvdaLanguage))
			{
				return false;
			}

			var nvdaLanguage = (NvdaLanguage)obj;
			return nvdaLanguage.Value.Equals(this.Value, StringComparison.CurrentCulture);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
		/// </returns>
		public bool Equals(NvdaLanguage other)
		{
			return this.Value.Equals(other.Value, StringComparison.CurrentCulture);
		}
	}
}