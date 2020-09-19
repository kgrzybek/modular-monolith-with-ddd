CREATE VIEW [meetings].[v_Countriess]
AS
SELECT
    [Country].[Code],
    [Country].[Name]
FROM meetings.Countries AS [Country]
GO
