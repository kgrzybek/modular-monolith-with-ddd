using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid
{
    public class SubscriptionRenewalPaymentAsPaidNotificationHandler
        : INotificationHandler<SubscriptionRenewalPaymentPaidNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public SubscriptionRenewalPaymentAsPaidNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(SubscriptionRenewalPaymentPaidNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(
                new RenewSubscriptionCommand(
                    Guid.NewGuid(),
                    notification.DomainEvent.SubscriptionId,
                    notification.DomainEvent.SubscriptionRenewalPaymentId));
        }
    }
}