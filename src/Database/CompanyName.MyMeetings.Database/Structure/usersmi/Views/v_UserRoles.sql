CREATE VIEW [usersmi].[v_UserRoles]
AS
SELECT [UserRole].[UserId]	AS [UserId],
       [Role].[Name]		AS [RoleCode]
  FROM [usersmi].[UserRoles] AS [UserRole] 
	   INNER JOIN [usersmi].[Roles] AS [Role]
			   ON [UserRole].[RoleId] = [Role].[Id]
GO