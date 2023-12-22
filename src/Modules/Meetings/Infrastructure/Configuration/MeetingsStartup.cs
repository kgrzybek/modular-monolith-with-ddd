using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember;
using CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Authentication;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.DataAccess;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Email;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.EventsBus;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Logging;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Mediation;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Quartz;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration
{
    public class MeetingsStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            IEventsBus eventsBus,
            long? internalProcessingPoolingInterval = null)
        {
            var moduleLogger = logger.ForContext("Module", "Meetings");

            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                moduleLogger,
                emailsConfiguration,
                eventsBus);

            QuartzStartup.Initialize(moduleLogger, internalProcessingPoolingInterval);

            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop()
        {
            QuartzStartup.StopQuartz();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            IEventsBus eventsBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Meetings")));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new AuthenticationModule());

            var domainNotificationsMap = new BiDictionary<string, Type>();
            domainNotificationsMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
            domainNotificationsMap.Add("MeetingGroupProposedNotification", typeof(MeetingGroupProposedNotification));
            domainNotificationsMap.Add("MeetingGroupCreatedNotification", typeof(MeetingGroupCreatedNotification));
            domainNotificationsMap.Add("MeetingAttendeeAddedNotification", typeof(MeetingAttendeeAddedNotification));
            domainNotificationsMap.Add("MemberCreatedNotification", typeof(MemberCreatedNotification));
            domainNotificationsMap.Add("MemberSubscriptionExpirationDateChangedNotification", typeof(MemberSubscriptionExpirationDateChangedNotification));
            domainNotificationsMap.Add("MeetingCommentLikedNotification", typeof(MeetingCommentLikedNotification));
            domainNotificationsMap.Add("MeetingCommentUnlikedNotification", typeof(MeetingCommentUnlikedNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            containerBuilder.RegisterModule(new EmailModule(emailsConfiguration));
            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            MeetingsCompositionRoot.SetContainer(_container);
        }
    }
}