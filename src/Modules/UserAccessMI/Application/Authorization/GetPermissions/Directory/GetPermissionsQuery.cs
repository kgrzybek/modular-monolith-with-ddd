﻿using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions.Directory;

public class GetPermissionsQuery : QueryBase<Result<IEnumerable<PermissionDto>>>
{
}