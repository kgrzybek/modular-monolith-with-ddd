using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    internal class MeetingGroupProposalAcceptedNotificationHandler : INotificationHandler<MeetingGroupProposalAcceptedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        internal MeetingGroupProposalAcceptedNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MeetingGroupProposalAcceptedNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(
                new CreateNewMeetingGroupCommand(
                    Guid.NewGuid(),
                    notification.DomainEvent.MeetingGroupProposalId));
        }
    }
}