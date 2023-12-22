using CompanyName.MyMeetings.BuildingBlocks.Application.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetAllMeetingGroupProposals
{
    public class GetAllMeetingGroupProposalsQuery : QueryBase<List<MeetingGroupProposalDto>>, IPagedQuery
    {
        public GetAllMeetingGroupProposalsQuery(int? page, int? perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        public int? Page { get; }

        public int? PerPage { get; }
    }
}