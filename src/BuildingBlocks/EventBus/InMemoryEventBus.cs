using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.BuildingBlocks.EventBus
{
    public sealed class InMemoryEventBus
    {
        static InMemoryEventBus()
        {
        }

        private InMemoryEventBus()
        {
            _handlers = new List<HandlerSubscription>();
        }

        public static InMemoryEventBus Instance { get; } = new InMemoryEventBus();

        private readonly List<HandlerSubscription> _handlers;

        public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
        {
            _handlers.Add(new HandlerSubscription(handler, typeof(T).FullName));
        }

        public void Publish<T>(T @event) where T : IntegrationEvent
        {
            var eventType = @event.GetType();

            var integrationEventHandlers = _handlers.Where(x => x.EventName == eventType.FullName).ToList();
         
            foreach (var integrationEventHandler in integrationEventHandlers)
            {
                if (integrationEventHandler.Handler is IIntegrationEventHandler<T> handler)
                {
                    handler.Handle(@event);
                }               
            }
        }

        private class HandlerSubscription
        {
            public HandlerSubscription(IIntegrationEventHandler handler, string eventName)
            {
                Handler = handler;
                EventName = eventName;
            }

            public IIntegrationEventHandler Handler { get;  }

            public string EventName { get; }
        }
    }
}