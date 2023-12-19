using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    /// <summary>
    /// Represents the identifier for a meeting group proposal.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MeetingGroupProposalId"/> class.
    /// </remarks>
    /// <param name="value">The value of the meeting group proposal identifier.</param>
    public class MeetingGroupProposalId(Guid value) : TypedIdValueBase(value)
    {
    }
}