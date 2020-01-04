using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Authentication;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.DataAccess;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.EventsBus;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Logging;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Mediation;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Quartz;
using Serilog.AspNetCore;
using ILogger = Serilog.ILogger;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration
{
    public class PaymentsStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "Payments");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger);

            QuartzStartup.Initialize(moduleLogger);

            EventsBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));

            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule());
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new AuthenticationModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            PaymentsCompositionRoot.SetContainer(_container);
        }
    }
}