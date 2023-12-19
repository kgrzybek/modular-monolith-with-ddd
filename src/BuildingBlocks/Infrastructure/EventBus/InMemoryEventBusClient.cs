using Serilog;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    /// <summary>
    /// Represents an in-memory event bus client that implements the <see cref="IEventsBus"/> interface.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="InMemoryEventBusClient"/> class.
    /// </remarks>
    /// <param name="logger">The logger used for logging.</param>
    public class InMemoryEventBusClient(ILogger logger) : IEventsBus
    {
        private readonly ILogger _logger = logger;

        /// <inheritdoc/>
        public void Dispose()
        {
        }

        /// <inheritdoc/>
        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            _logger.Information("Publishing {Event}", @event.GetType().FullName);
            await InMemoryEventBus.Instance.Publish(@event);
        }

        /// <inheritdoc/>
        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

        /// <inheritdoc/>
        public void StartConsuming()
        {
        }
    }
}