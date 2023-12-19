using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposals
{
    /// <summary>
    /// Handles the query to get a list of meeting group proposals.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GetMeetingGroupProposalsQueryHandler"/> class.
    /// </remarks>
    /// <param name="sqlConnectionFactory">The SQL connection factory.</param>
    internal class GetMeetingGroupProposalsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetMeetingGroupProposalsQuery, List<MeetingGroupProposalDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

        /// <summary>
        /// Handles the <see cref="GetMeetingGroupProposalsQuery"/> by retrieving a list of meeting group proposals.
        /// </summary>
        /// <param name="query">The query object containing the parameters for the query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="MeetingGroupProposalDto"/> objects.</returns>
        public async Task<List<MeetingGroupProposalDto>> Handle(GetMeetingGroupProposalsQuery query, CancellationToken cancellationToken)
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
                         "FROM [administration].[v_MeetingGroupProposals] AS [MeetingGroupProposal] ";

            return (await connection.QueryAsync<MeetingGroupProposalDto>(sql)).AsList();
        }
    }
}