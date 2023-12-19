using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Events
{
    /// <summary>
    /// Represents a base class for domain event notifications.
    /// </summary>
    /// <typeparam name="T">The type of the domain event.</typeparam>
    public class DomainNotificationBase<T> : IDomainEventNotification<T>
        where T : IDomainEvent
    {
        /// <summary>
        /// Gets the domain event associated with the notification.
        /// </summary>
        public T DomainEvent { get; }

        /// <summary>
        /// Gets the unique identifier of the notification.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainNotificationBase{T}"/> class.
        /// </summary>
        /// <param name="domainEvent">The domain event associated with the notification.</param>
        /// <param name="id">The unique identifier of the notification.</param>
        public DomainNotificationBase(T domainEvent, Guid id)
        {
            this.Id = id;
            this.DomainEvent = domainEvent;
        }
    }
}