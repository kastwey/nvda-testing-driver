// Copyright (C) 2020 Juan José Montiel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NvdaTestingDriver.Interfaces;

namespace NvdaTestingDriver
{
	/// <summary>
	/// Keeps track of all tasks in progress, to ensure that they have all been completed before releasing the resources associated with them.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public sealed class TrackingDisposer : IDisposable
	{
		private readonly LinkedList<Task> _tasks = new LinkedList<Task>();

		private readonly ITrackingDisposable _target;

		/// <summary>
		/// Initializes a new instance of the <see cref="TrackingDisposer" /> class.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <exception cref="ArgumentNullException">target</exception>
		/// <remarks>
		/// The supported class must implement ITrackingDisposable
		/// </remarks>
		public TrackingDisposer(ITrackingDisposable target)
		=> _target = target ?? throw new ArgumentNullException(nameof(target));

		/// <summary>
		/// Gets a value indicating whether this instance is disposed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
		/// </value>
		public bool IsDisposed { get; private set; } = false;

		internal ILogger Logger { get; set; }

		/// <summary>
		/// Add a task to the tracking list, returns false if disposed
		/// without a return value
		/// </summary>
		/// <param name="func">The function.</param>
		/// <param name="result">The result.</param>
		/// <returns><c>true</c> if the task is being tracked; <c>false, otherwise</c></returns>
		public bool Track(Func<Task> func, out Task result)
		{
			if (func is null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			lock (_tasks)
			{
				if (IsDisposed)
				{
					Logger.LogTrace("The object is being disposed. The task won't be executed. Exiting.");
					result = null;
					return false;
				}

				var task = func();
#pragma warning disable S1854 // Unused assignments should be removed. The node is used in local function.
				var node = _tasks.AddFirst(task);
#pragma warning restore S1854 // Unused assignments should be removed

				async Task Ending()
				{
					try
					{
						await task;
					}
					finally
					{
						var dispose = false;
						lock (_tasks)
						{
							_tasks.Remove(node);
							dispose = IsDisposed && _tasks.Count == 0;
						}

						if (dispose)
						{
							Logger.LogTrace("The object has been disposed (0 tasks pending to finish). Disposing driver...");
							await _target.FinishDisposeAsync();
						}
					}
				}

				result = Ending();
			}

			return true;
		}

		/// <summary>
		/// Add a task to the tracking list, returns false if disposed
		/// with  a return value
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="func">The function.</param>
		/// <param name="result">The result.</param>
		/// <returns>The task associated to this operation.</returns>
		public bool Track<TResult>(Func<Task<TResult>> func, out Task<TResult> result)
		{
			if (func is null)
			{
				throw new ArgumentNullException(nameof(func));
			}


			lock (_tasks)
			{
				if (IsDisposed)
				{
					Logger.LogTrace("The object is being disposed. The task won't be executed. Exiting.");
					result = null;
					return false;
				}

				var task = func();
#pragma warning disable S1854 // Unused assignments should be removed. The node is used in local function.
				var node = _tasks.AddFirst(task);
#pragma warning restore S1854 // Unused assignments should be removed

				async Task<TResult> Ending()
				{
					try
					{
						return await task;
					}
					finally
					{
						var dispose = false;
						lock (_tasks)
						{
							_tasks.Remove(node);
							dispose = IsDisposed && _tasks.Count == 0;
						}

						if (dispose)
						{
							Logger.LogTrace("The object has been disposed (0 tasks pending to finish). Disposing driver...");
							await _target.FinishDisposeAsync();
						}
					}
				}

				result = Ending();
			}

			return true;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			var dispose = false;
			Logger.LogTrace("TrackingDisposer: Starting to dispose...");

			lock (_tasks)
			{
				if (IsDisposed)
				{
					Logger.LogTrace("TrackingDisposer: The object has alreade been disposed.");
					return;
				}

				IsDisposed = true;
				Logger.LogTrace($"There are {_tasks.Count} tasks pending to finish.");
				dispose = _tasks.Count == 0;
			}

			if (dispose)
			{
				Logger.LogTrace("All tasks finished. Disposing driver.");
				_target.FinishDisposeAsync();
			}
		}
	}
}
