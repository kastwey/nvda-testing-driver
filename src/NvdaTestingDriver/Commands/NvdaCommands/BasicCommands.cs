// Copyright (C) 2019 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

namespace NvdaTestingDriver.Commands.NvdaCommands
{
	/// <summary>
	/// Class to group basic NVDA commands.
	/// </summary>
	public static class BasicCommands
	{
		/// <summary>
		/// Gets the command to exits NVDA.
		/// </summary>
		/// <value>
		/// The quit nvda command.
		/// </value>
		public static NvdaCommand QuitNvda => new NvdaCommand(new KeyCombination { Key.Nvda, Key.Q, }, nameof(QuitNvda));
	}
}
