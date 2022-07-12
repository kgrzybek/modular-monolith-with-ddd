CREATE VIEW [meetings].[v_Members]
AS
SELECT
    [Member].Id,
    [Member].[Name],
    [Member].[Login],
    [Member].[Email]
FROM meetings.Members AS [Member]
GO