using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace AccessibleDemo.Core.Tests.Logging
{
 public class MSTestLogger : ILogger, IDisposable
 {
		private readonly Action<string> output = Console.WriteLine;

		public IDisposable BeginScope<TState>(TState state) => this;

  public void Dispose()
  {
  }

  public bool IsEnabled(LogLevel logLevel) => true;

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) => output(formatter(state, exception));

	}
}
