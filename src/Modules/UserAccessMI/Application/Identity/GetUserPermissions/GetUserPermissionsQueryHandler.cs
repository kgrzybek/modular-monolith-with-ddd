using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Identity.GetUserPermissions;

internal class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, Result<IEnumerable<UserPermissionDto>>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<UserPermissionDto>>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT " +
                           "[UserPermission].[PermissionCode] AS [Code] " +
                           "FROM [usersmi].[v_UserPermissions] AS [UserPermission] " +
                           "WHERE [UserPermission].UserId = @UserId";

        var permissions = await connection.QueryAsync<UserPermissionDto>(sql, new { request.UserId });
        return Result.Ok(permissions);
    }
}