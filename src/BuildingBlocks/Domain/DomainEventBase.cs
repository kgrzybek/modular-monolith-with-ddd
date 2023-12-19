namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    /// <summary>
    /// Represents a base class for domain events.
    /// </summary>
    public class DomainEventBase : IDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier of the domain event.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the date and time when the domain event occurred.
        /// </summary>
        public DateTime OccurredOn { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventBase"/> class,
        /// with a new unique identifier and the current date and time.
        /// </summary>
        public DomainEventBase()
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = DateTime.UtcNow;
        }
    }
}