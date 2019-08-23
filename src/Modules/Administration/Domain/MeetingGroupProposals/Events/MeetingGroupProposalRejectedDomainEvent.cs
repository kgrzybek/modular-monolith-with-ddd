using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    internal class MeetingGroupProposalRejectedDomainEvent : DomainEventBase
    {
        internal MeetingGroupProposalId MeetingGroupProposalId { get; }

        internal MeetingGroupProposalRejectedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }
    }
}