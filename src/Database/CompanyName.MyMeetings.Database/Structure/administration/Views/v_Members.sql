CREATE VIEW [administration].[v_Members]
AS
SELECT
    [Member].[Id],
    [Member].[Login],
    [Member].[Email],
    [Member].[FirstName],
    [Member].[LastName],
    [Member].[Name]
FROM [administration].[Members] AS [Member]
GO