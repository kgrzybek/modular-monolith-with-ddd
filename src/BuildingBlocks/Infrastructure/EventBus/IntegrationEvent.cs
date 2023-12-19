using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    /// <summary>
    /// Represents an abstract base class for integration events.
    /// </summary>
    public abstract class IntegrationEvent : INotification
    {
        /// <summary>
        /// Gets the identifier of the integration event.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the date and time when the integration event occurred.
        /// </summary>
        public DateTime OccurredOn { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier of the integration event.</param>
        /// <param name="occurredOn">The date and time when the integration event occurred.</param>
        protected IntegrationEvent(Guid id, DateTime occurredOn)
        {
            this.Id = id;
            this.OccurredOn = occurredOn;
        }
    }
}