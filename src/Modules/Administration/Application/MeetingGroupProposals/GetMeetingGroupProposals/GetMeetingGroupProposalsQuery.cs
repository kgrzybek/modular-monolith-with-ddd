using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposals
{
    /// <summary>
    /// Represents a query to get a list of meeting group proposals.
    /// </summary>
    public class GetMeetingGroupProposalsQuery : QueryBase<List<MeetingGroupProposalDto>>
    {
        public GetMeetingGroupProposalsQuery()
        {
        }
    }
}