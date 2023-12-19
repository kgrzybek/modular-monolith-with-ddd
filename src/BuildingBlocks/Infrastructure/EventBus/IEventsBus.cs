namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    /// <summary>
    /// Represents an interface for an events bus that allows publishing and subscribing to integration events.
    /// </summary>
    public interface IEventsBus : IDisposable
    {
        /// <summary>
        /// Publishes an integration event to the events bus.
        /// </summary>
        /// <typeparam name="T">The type of the integration event.</typeparam>
        /// <param name="event">The integration event to publish.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Publish<T>(T @event)
            where T : IntegrationEvent;

        /// <summary>
        /// Subscribes a handler to handle integration events of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the integration event.</typeparam>
        /// <param name="handler">The integration event handler.</param>
        void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent;

        /// <summary>
        /// Starts consuming integration events from the events bus.
        /// </summary>
        void StartConsuming();
    }
}