using System.Collections.Specialized;
using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Authentication;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.DataAccess;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.EventsBus;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Logging;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Mediation;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Quartz;
using Serilog;
using Serilog.AspNetCore;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration
{
    public class AdministrationStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "Administration");

            ConfigureContainer(connectionString, executionContextAccessor, moduleLogger);

            QuartzStartup.Initialize(moduleLogger);

            EventsBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureContainer(string connectionString, 
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

            AdministrationCompositionRoot.SetContainer(_container);
        }
    }
}