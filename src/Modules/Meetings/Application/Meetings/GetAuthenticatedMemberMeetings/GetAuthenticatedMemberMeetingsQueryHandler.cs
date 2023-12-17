using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetAuthenticatedMemberMeetings
{
    internal class GetAuthenticatedMemberMeetingsQueryHandler : IQueryHandler<GetAuthenticatedMemberMeetingsQuery, List<MemberMeetingDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetAuthenticatedMemberMeetingsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<List<MemberMeetingDto>> Handle(GetAuthenticatedMemberMeetingsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<MemberMeetingDto>(
                $"""
                 SELECT [Meeting].[Id] AS [{nameof(MemberMeetingDto.MeetingId)}], 
                        [Meeting].[RoleCode] AS [{nameof(MemberMeetingDto.RoleCode)}], 
                        [Meeting].[LocationCity] AS [{nameof(MemberMeetingDto.LocationCity)}], 
                        [Meeting].[LocationAddress] AS [{nameof(MemberMeetingDto.LocationAddress)}], 
                        [Meeting].[LocationPostalCode] AS [{nameof(MemberMeetingDto.LocationPostalCode)}], 
                        [Meeting].[TermStartDate] AS [{nameof(MemberMeetingDto.TermStartDate)}], 
                        [Meeting].[TermEndDate] AS [{nameof(MemberMeetingDto.TermEndDate)}], 
                        [Meeting].[Title] AS [{nameof(MemberMeetingDto.Title)}] 
                 FROM [meetings].[v_MemberMeetings] AS [Meeting] 
                 WHERE [Meeting].[AttendeeId] = @AttendeeId AND [Meeting].[IsRemoved] = 0
                 """,
                new
                {
                    AttendeeId = _executionContextAccessor.UserId
                })).AsList();
        }
    }
}