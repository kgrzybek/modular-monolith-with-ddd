using System;
using System.Collections.Generic;
using CompanyName.MyMeetings.BuildingBlocks.Application.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposals
{
    public class GetMeetingGroupProposalsQuery : QueryBase<List<MeetingGroupProposalDto>>, IPagedQuery
    {
        public GetMeetingGroupProposalsQuery(int? page, int? perPage)
        {
            Page = page;
            PerPage = perPage;
        }

        public int? Page { get; }

        public int? PerPage { get; }
    }
}