using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.EventsBus
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
            var eventBus = RegistrationsCompositionRoot.BeginLifetimeScope().Resolve<IEventsBus>();

            //// SubscribeToIntegrationEvent<MemberCreatedIntegrationEvent>(eventBus, logger);
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