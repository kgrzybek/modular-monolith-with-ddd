using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;
using CompanyName.MyMeetings.Modules.Registrations.IntegrationEvents;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.EventsBus
{
    public static class EventsBusStartup
    {
        public static void Initialize(
            ILogger logger)
        {
            SubscribeToIntegrationEvents(logger);
        }

        private static void SubscribeToIntegrationEvents(ILogger logger)
        {
            var eventBus = PaymentsCompositionRoot.BeginLifetimeScope().Resolve<IEventsBus>();

            SubscribeToIntegrationEvent<MeetingGroupProposalAcceptedIntegrationEvent>(eventBus, logger);
            SubscribeToIntegrationEvent<NewUserRegisteredIntegrationEvent>(eventBus, logger);
            SubscribeToIntegrationEvent<MeetingAttendeeAddedIntegrationEvent>(eventBus, logger);
        }

        private static void SubscribeToIntegrationEvent<T>(IEventsBus eventBus, ILogger logger)
            where T : IntegrationEvent
        {
            logger.Information("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
            eventBus.Subscribe(
                new IntegrationEventGenericHandler<T>());
        }
    }
}