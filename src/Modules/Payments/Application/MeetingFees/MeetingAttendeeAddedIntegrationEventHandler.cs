using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.CreateMeetingFee;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees
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
                await _commandsScheduler.EnqueueAsync(new CreateMeetingFeeCommand(
                    Guid.NewGuid(),
                    notification.AttendeeId,
                    notification.MeetingId,
                    notification.FeeValue.Value,
                    notification.FeeCurrency));
            }
        }
    }
}