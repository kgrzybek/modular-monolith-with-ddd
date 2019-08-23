CREATE TABLE [users].[RolesToPermissions]
(
	[RoleCode] VARCHAR(50) NOT NULL,
	[PermissionCode] VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_RolesToPermissions_RoleCode_PermissionCode] PRIMARY KEY (RoleCode ASC, PermissionCode ASC)
)