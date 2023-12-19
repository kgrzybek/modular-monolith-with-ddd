CREATE VIEW [usersmi].[v_Users]
AS
SELECT
    [User].[Id],
    IIF([User].[LockoutEnabled] = 1, 0, 1) AS [IsActive],
    [User].[UserName] AS [Login],
    [User].[PasswordHash] AS [Password],
    [User].[Email],
    [User].[Name]
FROM [usersmi].[Users] AS [User]
GO