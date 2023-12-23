using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions
{
    internal class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT [UserPermission].[PermissionCode] AS [{nameof(UserPermissionDto.Code)}]
                                FROM [users].[v_UserPermissions] AS [UserPermission] 
                                WHERE [UserPermission].UserId = @UserId
                                """;
            var permissions = await connection.QueryAsync<UserPermissionDto>(sql, new { request.UserId });

            return permissions.AsList();
        }
    }
}