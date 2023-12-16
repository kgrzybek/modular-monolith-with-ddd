using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    public class MeetingGroupProposalAcceptedNotification : DomainNotificationBase<MeetingGroupProposalAcceptedDomainEvent>
    {
        public MeetingGroupProposalAcceptedNotification(MeetingGroupProposalAcceptedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}