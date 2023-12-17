using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposals
{
    internal class GetMeetingGroupProposalsQueryHandler : IQueryHandler<GetMeetingGroupProposalsQuery, List<MeetingGroupProposalDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingGroupProposalsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingGroupProposalDto>> Handle(GetMeetingGroupProposalsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT
                                    [MeetingGroupProposal].[Id] AS [{nameof(MeetingGroupProposalDto.Id)}],
                                    [MeetingGroupProposal].[Name] AS [{nameof(MeetingGroupProposalDto.Name)}],
                                    [MeetingGroupProposal].[ProposalUserId] AS [{nameof(MeetingGroupProposalDto.ProposalUserId)}],
                                    [MeetingGroupProposal].[LocationCity] AS [{nameof(MeetingGroupProposalDto.LocationCity)}],
                                    [MeetingGroupProposal].[LocationCountryCode] AS [{nameof(MeetingGroupProposalDto.LocationCountryCode)}],
                                    [MeetingGroupProposal].[Description] AS [{nameof(MeetingGroupProposalDto.Description)}],
                                    [MeetingGroupProposal].[ProposalDate] AS [{nameof(MeetingGroupProposalDto.ProposalDate)}],
                                    [MeetingGroupProposal].[StatusCode] AS [{nameof(MeetingGroupProposalDto.StatusCode)}],
                                    [MeetingGroupProposal].[DecisionDate] AS [{nameof(MeetingGroupProposalDto.DecisionDate)}],
                                    [MeetingGroupProposal].[DecisionUserId] AS [{nameof(MeetingGroupProposalDto.DecisionUserId)}],
                                    [MeetingGroupProposal].[DecisionCode] AS [{nameof(MeetingGroupProposalDto.DecisionCode)}],
                                    [MeetingGroupProposal].[DecisionRejectReason] AS [{nameof(MeetingGroupProposalDto.DecisionRejectReason)}]
                                FROM [administration].[v_MeetingGroupProposals] AS [MeetingGroupProposal]
                                """;

            return (await connection.QueryAsync<MeetingGroupProposalDto>(sql)).AsList();
        }
    }
}