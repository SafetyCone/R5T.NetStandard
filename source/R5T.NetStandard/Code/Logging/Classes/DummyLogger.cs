using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard
{
    /// <summary>
    /// Does nothing. Useful when a logger is required but you may not want to supply a logger.
    /// </summary>
    public class DummyLogger : ILogger
    {
        public static DummyLogger Instance { get; } = new DummyLogger();


        private DummyLogger()
        {
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // Do nothing.
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Always on.
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // Do nothing.
        }
    }
}
