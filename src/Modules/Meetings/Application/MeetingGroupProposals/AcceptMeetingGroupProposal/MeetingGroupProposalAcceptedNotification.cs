using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.SeedWork;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    public class MeetingGroupProposalAcceptedNotification : DomainNotificationBase<MeetingGroupProposalAcceptedDomainEvent>
    {
        internal MeetingGroupProposalId MeetingGroupProposalId { get; }
        public MeetingGroupProposalAcceptedNotification(MeetingGroupProposalAcceptedDomainEvent domainEvent) : base(domainEvent)
        {
            this.MeetingGroupProposalId = domainEvent.MeetingGroupProposalId;
        }
    }
}