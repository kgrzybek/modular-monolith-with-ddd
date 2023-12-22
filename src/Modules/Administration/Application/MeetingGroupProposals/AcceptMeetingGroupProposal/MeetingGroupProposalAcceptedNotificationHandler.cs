using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    public class MeetingGroupProposalAcceptedNotificationHandler : INotificationHandler<MeetingGroupProposalAcceptedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public MeetingGroupProposalAcceptedNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task Handle(MeetingGroupProposalAcceptedNotification notification, CancellationToken cancellationToken)
        {
            await _eventsBus.Publish(new MeetingGroupProposalAcceptedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.MeetingGroupProposalId.Value));
        }
    }
}