using Quartz.Logging;
using Serilog;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Quartz
{
    internal class SerilogLogProvider : ILogProvider
    {
        private readonly ILogger _logger;

        internal SerilogLogProvider(ILogger logger)
        {
            _logger = logger;
        }

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

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            throw new NotImplementedException();
        }
    }
}