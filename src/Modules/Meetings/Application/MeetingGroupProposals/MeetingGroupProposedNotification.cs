using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals
{
    public class MeetingGroupProposedNotification : DomainNotificationBase<MeetingGroupProposedDomainEvent>
    {
        public MeetingGroupProposalId MeetingGroupProposalId { get; }
        public MeetingGroupProposedNotification(MeetingGroupProposedDomainEvent domainEvent) : base(domainEvent)
        {
            this.MeetingGroupProposalId = domainEvent.Id;
        }
    }
}