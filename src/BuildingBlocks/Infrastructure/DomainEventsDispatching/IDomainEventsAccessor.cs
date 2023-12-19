using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Represents an interface for accessing domain events.
    /// </summary>
    public interface IDomainEventsAccessor
    {
        /// <summary>
        /// Gets all the domain events that have been recorded.
        /// </summary>
        /// <returns>A read-only collection of domain events.</returns>
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

        /// <summary>
        /// Clears all the recorded domain events.
        /// </summary>
        void ClearAllDomainEvents();
    }
}