using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals
{
    /// <summary>
    /// Represents an integration event that is raised when a meeting group proposal is accepted.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MeetingGroupProposalAcceptedIntegrationEvent"/> class.
    /// </remarks>
    /// <param name="id">The unique identifier of the integration event.</param>
    /// <param name="occurredOn">The date and time when the integration event occurred.</param>
    /// <param name="meetingGroupProposalId">The unique identifier of the meeting group proposal.</param>
    public class MeetingGroupProposalAcceptedIntegrationEvent(
        Guid id,
        DateTime occurredOn,
        Guid meetingGroupProposalId) : IntegrationEvent(id, occurredOn)
    {
        /// <summary>
        /// Gets the unique identifier of the meeting group proposal.
        /// </summary>
        public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
    }
}
