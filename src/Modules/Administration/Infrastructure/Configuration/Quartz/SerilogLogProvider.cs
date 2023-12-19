using Quartz.Logging;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Quartz
{
    /// <summary>
    /// Represents a log provider that uses Serilog for logging.
    /// </summary>
    internal class SerilogLogProvider : ILogProvider
    {
        private readonly ILogger _logger;

        internal SerilogLogProvider(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets a logger instance for the specified name.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        /// <returns>A logger instance.</returns>
        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (func == null)
                {
                    return true;
                }

                if (level == LogLevel.Debug || level == LogLevel.Trace)
                {
                    _logger.Debug(exception, func(), parameters);
                }

                if (level == LogLevel.Info)
                {
                    _logger.Information(exception, func(), parameters);
                }

                if (level == LogLevel.Warn)
                {
                    _logger.Warning(exception, func(), parameters);
                }

                if (level == LogLevel.Error)
                {
                    _logger.Error(exception, func(), parameters);
                }

                if (level == LogLevel.Fatal)
                {
                    _logger.Fatal(exception, func(), parameters);
                }

                return true;
            };
        }

        /// <inheritdoc/>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            throw new NotImplementedException();
        }
    }
}