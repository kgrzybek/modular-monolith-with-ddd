using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SendMeetingGroupCreatedEmail;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups
{
    internal class MeetingGroupCreatedSendEmailHandler : INotificationHandler<MeetingGroupCreatedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public MeetingGroupCreatedSendEmailHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MeetingGroupCreatedNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(
                new SendMeetingGroupCreatedEmailCommand(
                    Guid.NewGuid(),
                    notification.DomainEvent.MeetingGroupId,
                    notification.DomainEvent.CreatorId));
        }
    }
}