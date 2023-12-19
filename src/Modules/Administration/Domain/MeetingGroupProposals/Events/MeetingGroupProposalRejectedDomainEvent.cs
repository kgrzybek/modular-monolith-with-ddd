using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    /// <summary>
    /// Represents a domain event that is raised when a meeting group proposal is rejected.
    /// </summary>
    internal class MeetingGroupProposalRejectedDomainEvent : DomainEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingGroupProposalRejectedDomainEvent"/> class.
        /// </summary>
        /// <param name="meetingGroupProposalId">The ID of the rejected meeting group proposal.</param>
        internal MeetingGroupProposalRejectedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        /// <summary>
        /// Gets the ID of the rejected meeting group proposal.
        /// </summary>
        internal MeetingGroupProposalId MeetingGroupProposalId { get; }
    }
}