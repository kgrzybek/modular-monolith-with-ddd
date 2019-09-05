using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    public class MeetingGroupProposalVerificationRequestedDomainEvent : DomainEventBase
    {
        internal MeetingGroupProposalVerificationRequestedDomainEvent(MeetingGroupProposalId id)
        {   
            Id = id;
        }

        public MeetingGroupProposalId Id { get; }
    }
}