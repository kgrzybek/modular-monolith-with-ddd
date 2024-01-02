CREATE VIEW [registrations].[v_UserRegistrations]
AS
SELECT
    [UserRegistration].[Id],
    [UserRegistration].[Login],
    [UserRegistration].[Email],
    [UserRegistration].[FirstName],
    [UserRegistration].[LastName],
    [UserRegistration].[Name],
    [UserRegistration].[StatusCode],
    [UserRegistration].[Password]
FROM [registrations].[UserRegistrations] AS [UserRegistration]
GO