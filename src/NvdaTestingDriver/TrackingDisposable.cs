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
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NvdaTestingDriver.Interfaces;

namespace NvdaTestingDriver
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
	/// <summary>
	/// Abstract class to be inherited by classes that want to be disposable asynchronous.
	/// </summary>
	/// <seealso cref="NvdaTestingDriver.Interfaces.ITrackingDisposable" />
	public abstract class TrackingDisposable : ITrackingDisposable
#pragma warning restore CA1063 // Implement IDisposable Correctly
	{
		private readonly TrackingDisposer _disposer;

		private ILogger _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="TrackingDisposable"/> class.
		/// </summary>
		protected TrackingDisposable()
		=> _disposer = new TrackingDisposer(this);

		/// <summary>
		/// Gets or sets the logger service.
		/// </summary>
		/// <value>
		/// The logger.
		/// </value>
		protected ILogger Logger
		{
			get => _logger;
			set
			{
				_logger = value;
				_disposer.Logger = value;
			}
		}

		/// <summary>
		/// Finishes the dispose asynchronous.
		/// </summary>
		/// <returns>The task associated to this operation</returns>
		Task ITrackingDisposable.FinishDisposeAsync()
		{
			FinishDispose();
			return FinishDisposeAsync();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose()
		=> _disposer.Dispose();

		/// <summary>
		/// Finishes the dispose.
		/// </summary>
		protected virtual void FinishDispose()
		{
		}

		/// <summary>
		/// Finishes the dispose asynchronous.
		/// </summary>
		/// <returns>The task associated to this operation</returns>
		protected virtual Task FinishDisposeAsync()
		=> Task.CompletedTask;

		/// <summary>
		/// Tracks the specified function.
		/// </summary>
		/// <param name="func">The function.</param>
		/// <returns>The task associated to this operation</returns>
		/// <exception cref="ObjectDisposedException">TrackingDisposable</exception>
		protected Task Track(Func<Task> func)
		=> _disposer.Track(func, out var result)
			? result
			: throw new ObjectDisposedException(nameof(TrackingDisposable));

		/// <summary>
		/// Tracks the specified function.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="func">The function.</param>
		/// <returns>The task associated to this operation</returns>
		/// <exception cref="ObjectDisposedException">TrackingDisposable</exception>
		protected Task<TResult> Track<TResult>(Func<Task<TResult>> func)
		=> _disposer.Track(func, out var result)
			? result
			: throw new ObjectDisposedException(nameof(TrackingDisposable));
	}
}
