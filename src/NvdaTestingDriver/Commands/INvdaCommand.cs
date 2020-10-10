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
	/// Interface which include all properties used by NvdaDriver command functions.
	/// </summary>
	public interface INvdaCommand
	{
		/// <summary>
		/// Gets the desktop combination set.
		/// </summary>
		/// <value>
		/// The desktop combination set.
		/// </value>
		List<KeyCombination> DesktopCombinationSet { get; }

		/// <summary>
		/// Gets the laptop combination set.
		/// </summary>
		/// <value>
		/// The laptop combination set.
		/// </value>
		List<KeyCombination> LaptopCombinationSet { get; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The command name.
		/// </value>
		string Name { get; }
	}
}