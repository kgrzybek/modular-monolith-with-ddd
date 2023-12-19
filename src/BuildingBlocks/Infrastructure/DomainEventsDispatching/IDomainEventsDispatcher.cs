namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Represents an interface for dispatching domain events.
    /// </summary>
    public interface IDomainEventsDispatcher
    {
        /// <summary>
        /// Dispatches the domain events asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DispatchEventsAsync();
    }
}