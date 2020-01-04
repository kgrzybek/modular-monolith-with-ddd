using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail
{
    internal class MeetingAttendeeAddedNotificationHandler : INotificationHandler<MeetingAttendeeAddedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        internal MeetingAttendeeAddedNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MeetingAttendeeAddedNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(
                new SendMeetingAttendeeAddedEmailCommand(
                    Guid.NewGuid(),
                    notification.AttendeeId, 
                    notification.MeetingId));
        }
    }
}