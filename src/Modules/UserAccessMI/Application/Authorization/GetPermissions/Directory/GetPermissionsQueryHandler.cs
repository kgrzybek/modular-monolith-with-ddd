using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions.Directory;

internal class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, Result<IEnumerable<PermissionDto>>>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetPermissionsQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IEnumerable<PermissionDto>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        using (var connection = _connectionFactory.CreateNewConnection())
        {
            string query = $@"
                    SELECT [P].[Code]           AS {nameof(PermissionDto.Code)},
                           [P].[Name]           AS {nameof(PermissionDto.Name)},
                           [P].[Description]    AS {nameof(PermissionDto.Description)}
                      FROM [usersmi].[Permissions] AS [P]";

            var permissions = await connection.QueryAsync<PermissionDto>(query, cancellationToken);
            return Result.Ok(permissions);
        }
    }
}