using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.RegisterPayment
{
    public class PaymentRegisteredNotificationHandler : INotificationHandler<PaymentRegisteredNotification>
    {
        private readonly IEventsBus _eventsBus;

        public PaymentRegisteredNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(PaymentRegisteredNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new PaymentRegisteredIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.MeetingGroupPaymentRegisterId.Value,
                notification.DomainEvent.DateTo));

            return Task.CompletedTask;
        }
    }
}