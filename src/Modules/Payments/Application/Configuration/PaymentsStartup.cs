using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Authentication;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.DataAccess;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.EventsBus;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Logging;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Mediation;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Quartz;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration
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