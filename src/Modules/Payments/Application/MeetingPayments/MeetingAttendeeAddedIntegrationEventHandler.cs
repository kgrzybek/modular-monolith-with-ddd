using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments.CreateMeetingPayment;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments
{
    internal class MeetingAttendeeAddedIntegrationEventHandler : INotificationHandler<MeetingAttendeeAddedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        internal MeetingAttendeeAddedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MeetingAttendeeAddedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            if (notification.FeeValue.HasValue)
            {
                await _commandsScheduler.EnqueueAsync(new CreateMeetingPaymentCommand(
                    Guid.NewGuid(),
                    new PayerId(notification.AttendeeId), 
                    new MeetingId(notification.MeetingId),
                    notification.FeeValue.Value, 
                    notification.FeeCurrency));
            }
        }
    }
}