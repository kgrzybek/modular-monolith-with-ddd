using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid
{
    public class SubscriptionPaymentPaidNotificationHandler : INotificationHandler<SubscriptionPaymentPaidNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public SubscriptionPaymentPaidNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(SubscriptionPaymentPaidNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(
                new CreateSubscriptionCommand(
                    Guid.NewGuid(),
                    notification.DomainEvent.SubscriptionPaymentId));
        }
    }
}