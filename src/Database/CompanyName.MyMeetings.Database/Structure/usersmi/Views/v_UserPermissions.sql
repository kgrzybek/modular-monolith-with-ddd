CREATE VIEW [usersmi].[v_UserPermissions]
AS
SELECT 
	DISTINCT
	[UserRole].[UserId]				 AS [UserId],
	[RoleClaim].[ClaimValue]		 AS [PermissionCode]
FROM [usersmi].UserRoles AS [UserRole]
	 INNER JOIN [usersmi].[RoleClaims] AS [RoleClaim]
			 ON [UserRole].[RoleId] = [RoleClaim].[RoleId]
GO