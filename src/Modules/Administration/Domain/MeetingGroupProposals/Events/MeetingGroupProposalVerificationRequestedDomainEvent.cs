using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    internal class MeetingGroupProposalVerificationRequestedDomainEvent : DomainEventBase
    {
        internal MeetingGroupProposalVerificationRequestedDomainEvent(MeetingGroupProposalId id)
        {   
            Id = id;
        }

        internal MeetingGroupProposalId Id { get;  }
    }
}