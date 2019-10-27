using System;
using System.Collections.Generic;
using System.Linq;

using NvdaTestingDriver.Commands;

namespace NvdaTestingDriver.Extensions
{
	/// <summary>
	/// Extension methods for INvdaCommand interface.
	/// </summary>
	public static class NvdaCommandExtensions
	{
		/// <summary>
		/// Gets the command description.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>The command description, bassed on its name, or if does not exists, in its combination set</returns>
		public static string GetDescription(this INvdaCommand command)
		{
			if (command is null)
			{
				throw new ArgumentNullException(nameof(command));
			}

			if (!string.IsNullOrWhiteSpace(command.Name))
			{
				return command.Name;
			}

			return "Unknown name." + Environment.NewLine +
				"Desktop combinations: " +
				GetCombinationDesc(command.DesktopCombinationSet) +
				"." + (command.LaptopCombinationSet != null ? "Lactop combinations: " + GetCombinationDesc(command.LaptopCombinationSet) : string.Empty);
		}

		/// <summary>
		/// Gets the combination description.
		/// </summary>
		/// <param name="combinations">The combinations.</param>
		/// <returns>The combination description, bassed on its key names.</returns>
		private static string GetCombinationDesc(List<KeyCombination> combinations)
		{
			return string.Join(", ", combinations.Select(cs => "(" + cs.GetDescription() + ")"));
		}
	}
}