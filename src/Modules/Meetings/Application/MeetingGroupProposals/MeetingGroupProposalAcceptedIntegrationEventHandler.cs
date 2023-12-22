using CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals
{
    public class MeetingGroupProposalAcceptedIntegrationEventHandler :
        INotificationHandler<MeetingGroupProposalAcceptedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public MeetingGroupProposalAcceptedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MeetingGroupProposalAcceptedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new AcceptMeetingGroupProposalCommand(
                Guid.NewGuid(),
                notification.MeetingGroupProposalId));
        }
    }
}