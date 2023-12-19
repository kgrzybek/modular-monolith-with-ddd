CREATE VIEW [usersmi].[v_UserRegistrations]
AS
SELECT
    [UserRegistration].[Id],
    [UserRegistration].[UserName],
    [UserRegistration].[Email],
    [UserRegistration].[FirstName],
    [UserRegistration].[LastName],
    [UserRegistration].[Name],
    [UserRegistration].[StatusCode]
FROM [usersmi].[UserRegistrations] AS [UserRegistration]
GO