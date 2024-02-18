using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;
using CompanyName.MyMeetings.Modules.Registrations.IntegrationEvents;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.EventsBus
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
            var eventBus = MeetingsCompositionRoot.BeginLifetimeScope().Resolve<IEventsBus>();

            SubscribeToIntegrationEvent<MeetingGroupProposalAcceptedIntegrationEvent>(eventBus, logger);
            SubscribeToIntegrationEvent<SubscriptionExpirationDateChangedIntegrationEvent>(eventBus, logger);
            SubscribeToIntegrationEvent<NewUserRegisteredIntegrationEvent>(eventBus, logger);
            SubscribeToIntegrationEvent<MeetingFeePaidIntegrationEvent>(eventBus, logger);
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