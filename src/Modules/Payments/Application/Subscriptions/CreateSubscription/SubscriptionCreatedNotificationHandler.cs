using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription
{
    public class SubscriptionCreatedNotificationHandler : INotificationHandler<SubscriptionCreatedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public SubscriptionCreatedNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task Handle(SubscriptionCreatedNotification notification, CancellationToken cancellationToken)
        {
            await _eventsBus.Publish(new SubscriptionExpirationDateChangedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.PayerId,
                notification.DomainEvent.ExpirationDate));
        }
    }
}