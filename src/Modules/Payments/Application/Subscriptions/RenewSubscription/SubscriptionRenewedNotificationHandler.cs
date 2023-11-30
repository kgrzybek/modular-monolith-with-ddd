using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription
{
    public class SubscriptionRenewedNotificationHandler : INotificationHandler<SubscriptionRenewedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public SubscriptionRenewedNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task Handle(SubscriptionRenewedNotification notification, CancellationToken cancellationToken)
        {
            await _eventsBus.Publish(new SubscriptionExpirationDateChangedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.PayerId,
                notification.DomainEvent.ExpirationDate));
        }
    }
}