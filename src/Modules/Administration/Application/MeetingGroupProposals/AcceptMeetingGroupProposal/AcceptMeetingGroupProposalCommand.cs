using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    /// <summary>
    /// Represents a command to accept a meeting group proposal.
    /// </summary>
    /// <remarks>
    /// Creates a new instance of the <see cref="AcceptMeetingGroupProposalCommand"/> class.
    /// </remarks>
    /// <param name="meetingGroupProposalId">The ID of the meeting group proposal to accept.</param>
    public class AcceptMeetingGroupProposalCommand(Guid meetingGroupProposalId) : CommandBase
    {
        internal Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
    }
}