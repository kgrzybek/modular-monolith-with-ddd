using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMemberMeetingGroupProposals
{
    internal class GetMemberMeetingGroupProposalsQueryHandler : IQueryHandler<GetMemberMeetingGroupProposalsQuery, List<MeetingGroupProposalDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IMemberContext _memberContext;

        public GetMemberMeetingGroupProposalsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IMemberContext memberContext)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _memberContext = memberContext;
        }

        public async Task<List<MeetingGroupProposalDto>> Handle(GetMemberMeetingGroupProposalsQuery query, CancellationToken cancellationToken)
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
                                   [MeetingGroupProposal].[StatusCode] AS [{nameof(MeetingGroupProposalDto.StatusCode)}] 
                               FROM [meetings].[v_MeetingGroupProposals] AS [MeetingGroupProposal] 
                               WHERE [MeetingGroupProposal].ProposalUserId = @MemberId 
                               ORDER BY [MeetingGroupProposal].[Name]
                               """;

            var meetingGroupProposals = await connection.QueryAsync<MeetingGroupProposalDto>(
                sql,
                new
                {
                    MemberId = _memberContext.MemberId.Value
                });

            return meetingGroupProposals.AsList();
        }
    }
}