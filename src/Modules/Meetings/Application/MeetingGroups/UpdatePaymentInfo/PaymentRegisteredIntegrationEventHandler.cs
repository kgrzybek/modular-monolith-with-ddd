using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.UpdatePaymentInfo
{
    public class PaymentRegisteredIntegrationEventHandler : INotificationHandler<PaymentRegisteredIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public PaymentRegisteredIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public Task Handle(PaymentRegisteredIntegrationEvent notification, CancellationToken cancellationToken)
        {
            _commandsScheduler.EnqueueAsync(new UpdateMeetingGroupPaymentInfoCommand(
                Guid.NewGuid(),
                notification.MeetingGroupPaymentRegisterId,
                notification.DateTo));

            return Task.CompletedTask;
        }
    }
}