using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    public class MeetingGroupProposalAcceptedNotification : DomainNotificationBase<MeetingGroupProposalAcceptedDomainEvent>
    {
        internal MeetingGroupProposalId MeetingGroupProposalId { get; }

        public MeetingGroupProposalAcceptedNotification(MeetingGroupProposalAcceptedDomainEvent domainEvent, Guid id) : base(domainEvent, id)
        {
            this.MeetingGroupProposalId = domainEvent.MeetingGroupProposalId;
        }
    }
}