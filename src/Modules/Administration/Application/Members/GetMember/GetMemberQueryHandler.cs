using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.GetMember
{
    internal class GetMemberQueryHandler : IQueryHandler<GetMemberQuery, MemberDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMemberQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MemberDto> Handle(GetMemberQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = $"""
                               SELECT
                                  [Member].[Id] AS [{nameof(MemberDto.Id)}],
                                  [Member].[Login] AS [{nameof(MemberDto.Login)}],
                                  [Member].[Email] AS [{nameof(MemberDto.Email)}],
                                  [Member].[FirstName] AS [{nameof(MemberDto.FirstName)}],
                                  [Member].[LastName] AS [{nameof(MemberDto.LastName)}],
                                  [Member].[Name] AS [{nameof(MemberDto.Name)}]
                               FROM [administration].[v_Members] AS [Member]
                               WHERE [Member].[Id] = @MemberId   
                              """;

            return await connection.QuerySingleAsync<MemberDto>(sql, new { query.MemberId });
        }
    }
}