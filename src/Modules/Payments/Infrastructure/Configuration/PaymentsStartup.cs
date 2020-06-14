using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore;
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
            ILogger logger,
            IEventsBus eventsBus,
            bool runQuartz = true)
        {
            var moduleLogger = logger.ForContext("Module", "Payments");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger, eventsBus, runQuartz);

            if (runQuartz)
            {
                QuartzStartup.Initialize(moduleLogger);
            }

            EventsBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            IEventsBus eventsBus,
            bool runQuartz = true)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));

            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new AuthenticationModule());
            containerBuilder.RegisterModule(new OutboxModule());
            
            if (runQuartz)
            {
                containerBuilder.RegisterModule(new QuartzModule());
            }

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            PaymentsCompositionRoot.SetContainer(_container);

            RunEventsProjectors();
        }

        private static void RunEventsProjectors()
        {
            var subscriptionsManager =_container.Resolve<SubscriptionsManager>();

            subscriptionsManager.Start();
        }
    }
}