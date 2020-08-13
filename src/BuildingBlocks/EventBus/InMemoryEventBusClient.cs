using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Serilog;

namespace CompanyName.MyMeetings.BuildingBlocks.EventBus
{
    public class InMemoryEventBusClient : IEventsBus
    {
        private readonly ILogger _logger;

        public InMemoryEventBusClient(ILogger logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {

        }

        public void Publish<T>(T @event) where T : IntegrationEvent
        {
            _logger.Information("Publishing {Event}", @event.GetType().FullName);
            InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

        public void StartConsuming()
        {

        }
    }
}
