using DbUp.Engine.Output;
using Serilog;

namespace DatabaseMigrator
{
    internal class SerilogUpgradeLog : IUpgradeLog
    {
        private readonly ILogger _logger;

        public SerilogUpgradeLog(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteInformation(string format, params object[] args)
        {
            _logger.Information(format, args);
        }

        public void WriteError(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            _logger.Warning(format, args);
        }
    }
}