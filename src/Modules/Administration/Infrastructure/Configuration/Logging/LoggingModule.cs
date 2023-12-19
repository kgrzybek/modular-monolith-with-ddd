using Autofac;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Logging
{
    /// <summary>
    /// Represents a module for configuring logging in the Administration module.
    /// </summary>
    internal class LoggingModule : Autofac.Module
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingModule"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        internal LoggingModule(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Loads the logging module into the Autofac container.
        /// </summary>
        /// <param name="builder">The Autofac container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_logger)
                .As<ILogger>()
                .SingleInstance();
        }
    }
}