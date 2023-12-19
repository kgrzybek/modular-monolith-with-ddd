namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    /// <summary>
    /// Represents an interface for handling integration events.
    /// </summary>
    /// <typeparam name="TIntegrationEvent">The type of integration event to handle.</typeparam>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// Handles the specified integration event.
        /// </summary>
        /// <param name="event">The integration event to handle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Handle(TIntegrationEvent @event);
    }

    /// <summary>
    /// Represents an interface for integration event handlers.
    /// </summary>
    public interface IIntegrationEventHandler
    {
    }
}