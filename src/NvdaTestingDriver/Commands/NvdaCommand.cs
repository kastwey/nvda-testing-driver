// Copyright (C) 2020 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System.Collections.Generic;

namespace NvdaTestingDriver.Commands
{
	/// <summary>
	/// Store NVDA command information.
	/// </summary>
	/// <seealso cref="NvdaTestingDriver.Commands.INvdaCommand" />
	public class NvdaCommand : INvdaCommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaCommand"/> class.
		/// </summary>
		/// <param name="desktopCombination">The desktop combination.</param>
		/// <param name="name">The name.</param>
		public NvdaCommand(KeyCombination desktopCombination, string name)
			: this(desktopCombination, null, name)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaCommand" /> class.
		/// </summary>
		/// <param name="desktopCombination">The desktop combination.</param>
		/// <param name="laptopCombination">The laptop combination.</param>
		/// <param name="name">The name.</param>
		public NvdaCommand(KeyCombination desktopCombination, KeyCombination laptopCombination = null, string name = null)
		{
			DesktopCombinationSet.Add(desktopCombination);
			if (laptopCombination != null)
			{
				LaptopCombinationSet.Add(laptopCombination);
			}
			else
			{
				LaptopCombinationSet.Add(desktopCombination);
			}

			Name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NvdaCommand"/> class.
		/// </summary>
		/// <param name="desktopCombinationSet">The desktop combination set.</param>
		/// <param name="laptopCombinationSet">The laptop combination set.</param>
		public NvdaCommand(List<KeyCombination> desktopCombinationSet, List<KeyCombination> laptopCombinationSet = null)
		{
			DesktopCombinationSet.AddRange(desktopCombinationSet);
			if (laptopCombinationSet != null)
			{
				LaptopCombinationSet.AddRange(laptopCombinationSet);
			}
			else
			{
				LaptopCombinationSet.AddRange(desktopCombinationSet);
			}
		}

		/// <summary>
		/// Gets the desktop combination set.
		/// </summary>
		/// <value>
		/// The desktop combination set.
		/// </value>
		public List<KeyCombination> DesktopCombinationSet { get; private set; } = new List<KeyCombination>();

		/// <summary>
		/// Gets the laptop combination set.
		/// </summary>
		/// <value>
		/// The laptop combination set.
		/// </value>
		public List<KeyCombination> LaptopCombinationSet { get; private set; } = new List<KeyCombination>();

		/// <summary>
		/// Gets or sets the command name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }
	}
}
