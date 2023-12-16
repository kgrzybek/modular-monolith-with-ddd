using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals
{
    public class MeetingGroupProposedNotificationHandler : INotificationHandler<MeetingGroupProposedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public MeetingGroupProposedNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task Handle(MeetingGroupProposedNotification notification, CancellationToken cancellationToken)
        {
            await _eventsBus.Publish(new MeetingGroupProposedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.MeetingGroupProposalId.Value,
                notification.DomainEvent.Name,
                notification.DomainEvent.Description,
                notification.DomainEvent.LocationCity,
                notification.DomainEvent.LocationCountryCode,
                notification.DomainEvent.ProposalUserId.Value,
                notification.DomainEvent.ProposalDate));
        }
    }
}