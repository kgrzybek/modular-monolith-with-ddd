using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Events
{
    /// <summary>
    /// Represents a notification for a domain event.
    /// </summary>
    /// <typeparam name="TEventType">The type of the domain event.</typeparam>
    public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
    {
        /// <summary>
        /// Gets the domain event.
        /// </summary>
        TEventType DomainEvent { get; }
    }

    /// <summary>
    /// Represents a domain event notification.
    /// </summary>
    public interface IDomainEventNotification : INotification
    {
        /// <summary>
        /// Gets the unique identifier of the domain event.
        /// </summary>
        Guid Id { get; }
    }
}