using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.GetMember
{
    /// <summary>
    /// Handles the <see cref="GetMemberQuery"/>  and returns a <see cref="MemberDto"/>.
    /// </summary>
    internal class GetMemberQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetMemberQuery, MemberDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

        /// <summary>
        /// Handles the <see cref="GetMemberQuery"/> and retrieves a <see cref="MemberDto"/>.
        /// </summary>
        /// <param name="query">The <see cref="GetMemberQuery"/> to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="MemberDto"/>.</returns>
        public async Task<MemberDto> Handle(GetMemberQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql = "SELECT " +
                      $"[Member].[Id] AS [{nameof(MemberDto.Id)}], " +
                      $"[Member].[Login] AS [{nameof(MemberDto.Login)}], " +
                      $"[Member].[Email] AS [{nameof(MemberDto.Email)}], " +
                      $"[Member].[FirstName] AS [{nameof(MemberDto.FirstName)}], " +
                      $"[Member].[LastName] AS [{nameof(MemberDto.LastName)}], " +
                      $"[Member].[Name] AS [{nameof(MemberDto.Name)}] " +
                      "FROM [administration].[v_Members] AS [Member] " +
                      "WHERE [Member].[Id] = @MemberId";

            return await connection.QuerySingleAsync<MemberDto>(sql, new { query.MemberId });
        }
    }
}