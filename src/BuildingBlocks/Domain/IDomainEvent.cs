using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    /// <summary>
    /// Represents a domain event.
    /// </summary>
    public interface IDomainEvent : INotification
    {
        /// <summary>
        /// Gets the unique identifier of the domain event.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the date and time when the domain event occurred.
        /// </summary>
        DateTime OccurredOn { get; }
    }
}