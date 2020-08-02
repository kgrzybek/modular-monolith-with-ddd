using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    internal class MeetingGroupProposalRejectedDomainEvent : DomainEventBase
    {
        internal MeetingGroupProposalRejectedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        internal MeetingGroupProposalId MeetingGroupProposalId { get; }
    }
}