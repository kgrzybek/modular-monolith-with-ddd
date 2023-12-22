using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetAllMeetingGroupProposals
{
    internal class GetAllMeetingGroupProposalsQueryHandler : IQueryHandler<GetAllMeetingGroupProposalsQuery, List<MeetingGroupProposalDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllMeetingGroupProposalsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingGroupProposalDto>> Handle(GetAllMeetingGroupProposalsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var parameters = new DynamicParameters();
            var pageData = PagedQueryHelper.GetPageData(query);
            parameters.Add(nameof(PagedQueryHelper.Offset), pageData.Offset);
            parameters.Add(nameof(PagedQueryHelper.Next), pageData.Next);

            var sql = $"""
                       SELECT
                           [MeetingGroupProposal].[Id] AS [{nameof(MeetingGroupProposalDto.Id)}],
                           [MeetingGroupProposal].[Name] AS [{nameof(MeetingGroupProposalDto.Name)}],
                           [MeetingGroupProposal].[ProposalUserId] AS [{nameof(MeetingGroupProposalDto.ProposalUserId)}],
                           [MeetingGroupProposal].[LocationCity] AS [{nameof(MeetingGroupProposalDto.LocationCity)}],
                           [MeetingGroupProposal].[LocationCountryCode] AS [{nameof(MeetingGroupProposalDto.LocationCountryCode)}],
                           [MeetingGroupProposal].[Description] AS [{nameof(MeetingGroupProposalDto.Description)}],
                           [MeetingGroupProposal].[ProposalDate] AS [{nameof(MeetingGroupProposalDto.ProposalDate)}],
                           [MeetingGroupProposal].[StatusCode] AS [{nameof(MeetingGroupProposalDto.StatusCode)}]
                       FROM [meetings].[v_MeetingGroupProposals] AS [MeetingGroupProposal]
                       ORDER BY [MeetingGroupProposal].[Name]
                       """;

            sql = PagedQueryHelper.AppendPageStatement(sql);

            var meetingGroupProposals = await connection.QueryAsync<MeetingGroupProposalDto>(sql, parameters);

            return meetingGroupProposals.AsList();
        }
    }
}