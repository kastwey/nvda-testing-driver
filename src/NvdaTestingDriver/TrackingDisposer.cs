using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using NvdaTestingDriver.Interfaces;

namespace NvdaTestingDriver
{
	public class TrackingDisposer : IDisposable
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

		/// <summary>
		/// Add a task to the tracking list, returns false if disposed
		/// without a return value
		/// </summary>
		/// <param name="func">The function.</param>
		/// <param name="result">The result.</param>
		/// <returns></returns>
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
					result = null;
					return false;
				}

				var task = func();
				var node = _tasks.AddFirst(task);

				async Task Ending()
				{
					await task;
					var dispose = false;
					lock (_tasks)
					{
						_tasks.Remove(node);
						dispose = IsDisposed && _tasks.Count == 0;
					}

					if (dispose)
					{
						await _target.FinishDisposeAsync();
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
		/// <returns></returns>
		public bool Track<TResult>(Func<Task<TResult>> func, out Task<TResult> result)
		{
			lock (_tasks)
			{
				if (IsDisposed)
				{
					result = null;
					return false;
				}

				var task = func();
				var node = _tasks.AddFirst(task);

				async Task<TResult> Ending()
				{
					var res = await task;
					var dispose = false;
					lock (_tasks)
					{
						_tasks.Remove(node);
						dispose = IsDisposed && _tasks.Count == 0;
					}

					if (dispose)
					{
						await _target.FinishDisposeAsync();
					}

					return res;
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

			lock (_tasks)
			{
				if (IsDisposed)
				{
					return;
				}

				IsDisposed = true;
				dispose = _tasks.Count == 0;
			}

			if (dispose)
			{
				_target.FinishDisposeAsync();
			}
		}
	}
}