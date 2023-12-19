using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal
{
    /// <summary>
    /// Handles the query to retrieve a meeting group proposal.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GetMeetingGroupProposalQueryHandler"/> class.
    /// </remarks>
    /// <param name="sqlConnectionFactory">The SQL connection factory.</param>
    internal class GetMeetingGroupProposalQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetMeetingGroupProposalQuery, MeetingGroupProposalDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

        /// <summary>
        /// Handles the <see cref="GetMeetingGroupProposalQuery"/> by retrieving the meeting group proposal from the database.
        /// </summary>
        /// <param name="query">The <see cref="GetMeetingGroupProposalQuery"/> object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="MeetingGroupProposalDto"/> object representing the retrieved meeting group proposal.</returns>
        public async Task<MeetingGroupProposalDto> Handle(GetMeetingGroupProposalQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT " +
                         $"[MeetingGroupProposal].[Id] AS [{nameof(MeetingGroupProposalDto.Id)}], " +
                         $"[MeetingGroupProposal].[Name] AS [{nameof(MeetingGroupProposalDto.Name)}], " +
                         $"[MeetingGroupProposal].[ProposalUserId] AS [{nameof(MeetingGroupProposalDto.ProposalUserId)}], " +
                         $"[MeetingGroupProposal].[LocationCity] AS [{nameof(MeetingGroupProposalDto.LocationCity)}], " +
                         $"[MeetingGroupProposal].[LocationCountryCode] AS [{nameof(MeetingGroupProposalDto.LocationCountryCode)}], " +
                         $"[MeetingGroupProposal].[Description] AS [{nameof(MeetingGroupProposalDto.Description)}], " +
                         $"[MeetingGroupProposal].[ProposalDate] AS [{nameof(MeetingGroupProposalDto.ProposalDate)}], " +
                         $"[MeetingGroupProposal].[StatusCode] AS [{nameof(MeetingGroupProposalDto.StatusCode)}], " +
                         $"[MeetingGroupProposal].[DecisionDate] AS [{nameof(MeetingGroupProposalDto.DecisionDate)}], " +
                         $"[MeetingGroupProposal].[DecisionUserId] AS [{nameof(MeetingGroupProposalDto.DecisionUserId)}], " +
                         $"[MeetingGroupProposal].[DecisionCode] AS [{nameof(MeetingGroupProposalDto.DecisionCode)}], " +
                         $"[MeetingGroupProposal].[DecisionRejectReason] AS [{nameof(MeetingGroupProposalDto.DecisionRejectReason)}] " +
                         "FROM [administration].[v_MeetingGroupProposals] AS [MeetingGroupProposal] " +
                         "WHERE [MeetingGroupProposal].[Id] = @MeetingGroupProposalId";

            return await connection.QuerySingleAsync<MeetingGroupProposalDto>(sql, new { query.MeetingGroupProposalId });
        }
    }
}