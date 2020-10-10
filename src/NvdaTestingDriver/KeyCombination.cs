// Copyright (C) 2020 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System.Collections;
using System.Collections.Generic;

namespace NvdaTestingDriver
{
	/// <summary>
	/// Stores a key combination to be used in NVDA command class.
	/// </summary>
	public class KeyCombination : IEnumerable<Key>
	{
		private readonly List<Key> _keys;

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyCombination"/> class.
		/// </summary>
		public KeyCombination()
		{
			_keys = new List<Key>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyCombination"/> class.
		/// </summary>
		/// <param name="keys">The keys.</param>
		public KeyCombination(IEnumerable<Key> keys)
		{
			_keys = new List<Key>();
			_keys.AddRange(keys);
		}

		/// <summary>
		/// Adds the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		public void Add(Key key) => _keys.Add(key);

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<Key> GetEnumerator()
		{
			return _keys.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _keys.GetEnumerator();
		}
	}
}