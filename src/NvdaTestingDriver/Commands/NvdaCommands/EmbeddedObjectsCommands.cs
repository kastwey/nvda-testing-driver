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
	/// Class to group all NVDA  embedded object commands.
	/// </summary>
	public static class EmbeddedObjectsCommands
	{
		/// <summary>
		/// Gets the command to moves the focus out of the current embedded object and into the document that contains it.
		/// </summary>
		/// <value>
		/// The move to containing browse mode document command.
		/// </value>
		public static NvdaCommand MoveToContainingBrowseModeDocument => new NvdaCommand(
			new KeyCombination { Key.Nvda, Key.Control, Key.Space });
	}
}
