using System;
using System.Threading.Tasks;

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

		/// <summary>
		/// Initializes a new instance of the <see cref="TrackingDisposable"/> class.
		/// </summary>
		protected TrackingDisposable()
		=> _disposer = new TrackingDisposer(this);

		/// <summary>
		/// Finishes the dispose.
		/// </summary>
		protected virtual void FinishDispose()
  {
  }

		/// <summary>
		/// Finishes the dispose asynchronous.
		/// </summary>
		/// <returns></returns>
		protected virtual Task FinishDisposeAsync()
		=> Task.CompletedTask;

		/// <summary>
		/// Finishes the dispose asynchronous.
		/// </summary>
		/// <returns></returns>
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
		/// Tracks the specified function.
		/// </summary>
		/// <param name="func">The function.</param>
		/// <returns></returns>
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
		/// <returns></returns>
		/// <exception cref="ObjectDisposedException">TrackingDisposable</exception>
		protected Task<TResult> Track<TResult>(Func<Task<TResult>> func)
		=> _disposer.Track(func, out var result)
			? result
			: throw new ObjectDisposedException(nameof(TrackingDisposable));
	}
}