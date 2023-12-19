using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal
{
    /// <summary>
    /// Represents a query to get a meeting group proposal by its ID.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GetMeetingGroupProposalQuery"/> class.
    /// </remarks>
    /// <param name="meetingGroupProposalId">The ID of the meeting group proposal.</param>
    public class GetMeetingGroupProposalQuery(Guid meetingGroupProposalId) : QueryBase<MeetingGroupProposalDto>
    {
        /// <summary>
        /// Gets the ID of the meeting group proposal.
        /// </summary>
        public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
    }
}