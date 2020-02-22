using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork
{
    public class EventsBusMock : IEventsBus
    {
        private readonly IList<IntegrationEvent> _publishedEvents;

        public EventsBusMock()
        {
            _publishedEvents = new List<IntegrationEvent>();
        }
        public void Dispose()
        {

        }

        public void Publish<T>(T @event) where T : IntegrationEvent
        {
            _publishedEvents.Add(@event);
        }

        public T GetLastPublishedEvent<T>() where T : IntegrationEvent
        {
            return _publishedEvents.OfType<T>().Last();
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
        {

        }

        public void StartConsuming()
        {

        }
    }
}