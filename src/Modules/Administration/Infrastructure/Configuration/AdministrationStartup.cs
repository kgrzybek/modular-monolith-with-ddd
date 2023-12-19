using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification;
using CompanyName.MyMeetings.Modules.Administration.Application.Members.CreateMember;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Authentication;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.DataAccess;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventsBus;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Logging;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Mediation;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Quartz;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration
{
    /// <summary>
    /// Represents the startup class for the Administration module.
    /// </summary>
    public class AdministrationStartup
    {
        private static IContainer _container;

        /// <summary>
        /// Initializes the Administration module with the specified parameters.
        /// </summary>
        /// <param name="connectionString">The connection string for the database.</param>
        /// <param name="executionContextAccessor">The execution context accessor.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="eventsBus">The events bus.</param>
        /// <param name="internalProcessingPoolingInterval">The internal processing pooling interval (optional).</param>
        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            IEventsBus eventsBus,
            long? internalProcessingPoolingInterval = null)
        {
            var moduleLogger = logger.ForContext("Module", "Administration");

            ConfigureContainer(connectionString, executionContextAccessor, moduleLogger, eventsBus);

            QuartzStartup.Initialize(moduleLogger, internalProcessingPoolingInterval);

            EventsBusStartup.Initialize(moduleLogger);
        }

        /// <summary>
        /// Stops the administration module.
        /// </summary>
        public static void Stop()
        {
            QuartzStartup.StopQuartz();
        }

        /// <summary>
        /// Configures the container with the necessary modules and dependencies for the Administration module.
        /// </summary>
        /// <param name="connectionString">The connection string for the data access module.</param>
        /// <param name="executionContextAccessor">The execution context accessor.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="eventsBus">The events bus.</param>
        private static void ConfigureContainer(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            IEventsBus eventsBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            var loggerFactory = new Serilog.Extensions.Logging.SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));

            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new AuthenticationModule());

            var domainNotificationsMap = new BiDictionary<string, Type>();
            domainNotificationsMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            BiDictionary<string, Type> internalCommandsMap = new BiDictionary<string, Type>();
            internalCommandsMap.Add("CreateMember", typeof(CreateMemberCommand));
            internalCommandsMap.Add("RequestMeetingGroupProposalVerification", typeof(RequestMeetingGroupProposalVerificationCommand));
            containerBuilder.RegisterModule(new InternalCommandsModule(internalCommandsMap));

            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            AdministrationCompositionRoot.SetContainer(_container);
        }
    }
}