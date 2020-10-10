using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace AccessibleDemo.Core.Tests.Logging
{
 public class MSTestLoggerProvider : ILoggerProvider
 {
  public ILogger CreateLogger(string categoryName)
  {
			return new MSTestLogger();
  }

  public void Dispose()
  {
  }
 }
}
