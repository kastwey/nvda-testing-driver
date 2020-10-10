using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace AccessibleDemo.Core.Tests
{
	/// <summary>
	/// Factory for bootstrapping an application in memory for functional end to end
	///    tests.
	/// </summary>
	/// <typeparam name="TStartup">The startup class of the web app you want to start.</typeparam>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory{AccessibleDemo.Startup}" />
	public class SeleniumServerFactory<TStartup> : WebApplicationFactory<Startup> where TStartup : class
	{

		private IWebHost _host;

		/// <summary>
		/// Gets or sets the root URI.
		/// </summary>
		/// <value>
		/// The root URI.
		/// </value>
		public string RootUri { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SeleniumServerFactory{TStartup}"/> class.
		/// </summary>
		public SeleniumServerFactory()
		{
			ClientOptions.BaseAddress = new Uri("https://localhost"); //will follow redirects by default
		}

		/// <summary>
		/// Creates the <see cref="T:Microsoft.AspNetCore.TestHost.TestServer" /> with the bootstrapped application in <paramref name="builder" />.
		/// This is only called for applications using <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" />. Applications based on
		/// <see cref="T:Microsoft.Extensions.Hosting.IHostBuilder" /> will use <see cref="M:Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory`1.CreateHost(Microsoft.Extensions.Hosting.IHostBuilder)" /> instead.
		/// </summary>
		/// <param name="builder">The <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder" /> used to
		/// create the server.</param>
		/// <returns>
		/// The <see cref="T:Microsoft.AspNetCore.TestHost.TestServer" /> with the bootstrapped application.
		/// </returns>
		protected override TestServer CreateServer(IWebHostBuilder builder)
		{
			//Real TCP port
			_host = builder.Build();
			_host.Start();
			RootUri = _host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault(); //Last is ssl!

			//Fake Server we won't use
			return new TestServer(new WebHostBuilder().UseStartup<TStartup>());
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources;
		/// <see langword="false" /> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				_host?.Dispose();
			}
		}
	}
}
