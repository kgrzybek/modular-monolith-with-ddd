namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    /// <summary>
    /// Represents an in-memory event bus that allows subscribing to and publishing integration events.
    /// </summary>
    public sealed class InMemoryEventBus
    {
        static InMemoryEventBus()
        {
        }

        private InMemoryEventBus()
        {
            _handlersDictionary = new Dictionary<string, List<IIntegrationEventHandler>>();
        }

        /// <summary>
        /// Gets the singleton instance of the in-memory event bus.
        /// </summary>
        public static InMemoryEventBus Instance { get; } = new InMemoryEventBus();

        private readonly IDictionary<string, List<IIntegrationEventHandler>> _handlersDictionary;

        /// <summary>
        /// Subscribes a handler to handle integration events of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the integration event.</typeparam>
        /// <param name="handler">The integration event handler.</param>
        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            var eventType = typeof(T).FullName;
            if (eventType != null)
            {
                if (_handlersDictionary.TryGetValue(eventType, out List<IIntegrationEventHandler> value))
                {
                    var handlers = value;
                    handlers.Add(handler);
                }
                else
                {
                    _handlersDictionary.Add(eventType, new List<IIntegrationEventHandler> { handler });
                }
            }
        }

        /// <summary>
        /// Publishes an integration event to the events bus.
        /// </summary>
        /// <typeparam name="T">The type of the integration event.</typeparam>
        /// <param name="event">The integration event to publish.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            var eventType = @event.GetType().FullName;

            if (eventType == null)
            {
                return;
            }

            List<IIntegrationEventHandler> integrationEventHandlers = _handlersDictionary[eventType];

            foreach (var integrationEventHandler in integrationEventHandlers)
            {
                if (integrationEventHandler is IIntegrationEventHandler<T> handler)
                {
                    await handler.Handle(@event);
                }
            }
        }
    }
}