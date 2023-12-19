using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    /// <summary>
    /// Represents a domain event that is raised when a meeting group proposal verification is requested.
    /// </summary>
    public class MeetingGroupProposalVerificationRequestedDomainEvent : DomainEventBase
    {
        internal MeetingGroupProposalVerificationRequestedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        /// <summary>
        /// Gets the identifier of the meeting group proposal.
        /// </summary>
        public MeetingGroupProposalId MeetingGroupProposalId { get; }
    }
}