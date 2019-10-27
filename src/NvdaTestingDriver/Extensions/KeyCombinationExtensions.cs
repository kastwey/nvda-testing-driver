using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NvdaTestingDriver.Extensions
{
	/// <summary>
	/// Extension methods  for KeyCombination class.
	/// </summary>
	public static class KeyCombinationExtensions
	{
		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <param name="combination">The combination.</param>
		/// <returns>The key combination description</returns>
		public static string GetDescription(this KeyCombination combination)
		{
			return string.Join("+", combination.Select(k => k.Name));
		}
	}
}
