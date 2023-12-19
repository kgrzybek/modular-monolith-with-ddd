using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Domain;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.DataAccess;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Email;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.EventsBus;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Identity;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Logging;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Mediation;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Processing;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Quartz;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Services;
using Serilog;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration
{
    public class UserAccessStartup
    {
        private static IContainer? _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender,
            IEventsBus eventsBus,
            IUserAccessConfiguration userAccessConfiguration,
            long? internalProcessingPoolingInterval = null)
        {
            var moduleLogger = logger.ForContext("Module", "UserAccess");

            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                logger,
                emailsConfiguration,
                textEncryptionKey,
                emailSender,
                eventsBus,
                userAccessConfiguration);

            QuartzStartup.Initialize(moduleLogger, internalProcessingPoolingInterval);

            EventsBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender,
            IEventsBus eventsBus,
            IUserAccessConfiguration userAccessConfiguration)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "UserAccess")));

            var loggerFactory = new Serilog.Extensions.Logging.SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());

            var domainNotificationsMap = new BiDictionary<string, Type>();
            domainNotificationsMap.Add("NewUserRegisteredNotification", typeof(NewUserRegisteredNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new EmailModule(emailsConfiguration, emailSender));

            // containerBuilder.RegisterModule(new SecurityModule(textEncryptionKey));
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterModule(new IdentityModule());
            containerBuilder.RegisterModule(new ServicesModule(userAccessConfiguration));

            _container = containerBuilder.Build();

            UserAccessCompositionRoot.SetContainer(_container);
        }
    }
}