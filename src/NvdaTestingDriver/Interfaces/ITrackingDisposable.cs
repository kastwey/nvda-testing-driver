using System;
using System.Threading.Tasks;

namespace NvdaTestingDriver.Interfaces
{
	/// <summary>
	/// Track all added task to finish them before disposing the object
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface ITrackingDisposable : IDisposable
	{
		/// <summary>
		/// Finishes the dispose asynchronous.
		/// </summary>
		/// <returns>T</returns>
		Task FinishDisposeAsync();
	}
}
