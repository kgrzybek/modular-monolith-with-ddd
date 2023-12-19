using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Decorator class that dispatches domain events after handling a notification.
    /// </summary>
    /// <typeparam name="T">The type of notification to handle.</typeparam>
    public class DomainEventsDispatcherNotificationHandlerDecorator<T> : INotificationHandler<T>
        where T : INotification
    {
        private readonly INotificationHandler<T> _decorated;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventsDispatcherNotificationHandlerDecorator{T}"/> class.
        /// </summary>
        /// <param name="domainEventsDispatcher">The domain events dispatcher.</param>
        /// <param name="decorated">The decorated notification handler.</param>
        public DomainEventsDispatcherNotificationHandlerDecorator(
            IDomainEventsDispatcher domainEventsDispatcher,
            INotificationHandler<T> decorated)
        {
            _domainEventsDispatcher = domainEventsDispatcher;
            _decorated = decorated;
        }

        /// <summary>
        /// Handles the notification asynchronously, dispatching domain events after handling it.
        /// </summary>
        /// <param name="notification">The notification to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            await this._decorated.Handle(notification, cancellationToken);

            await this._domainEventsDispatcher.DispatchEventsAsync();
        }
    }
}