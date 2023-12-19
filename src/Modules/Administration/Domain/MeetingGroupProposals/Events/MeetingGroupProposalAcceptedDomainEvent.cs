using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    /// <summary>
    /// Represents a domain event that is raised when a meeting group proposal is accepted.
    /// </summary>
    public class MeetingGroupProposalAcceptedDomainEvent : DomainEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingGroupProposalAcceptedDomainEvent"/> class.
        /// </summary>
        /// <param name="meetingGroupProposalId">The ID of the accepted meeting group proposal.</param>
        public MeetingGroupProposalAcceptedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        /// <summary>
        /// Gets the ID of the accepted meeting group proposal.
        /// </summary>
        public MeetingGroupProposalId MeetingGroupProposalId { get; }
    }
}