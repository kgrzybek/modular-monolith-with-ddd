
CREATE VIEW [meetings].[v_Meetings]
AS
SELECT
	Meeting.[Id],
    Meeting.[Title],
    Meeting.[Description],
    Meeting.LocationAddress,
    Meeting.LocationCity,
    Meeting.LocationPostalCode,
    Meeting.TermStartDate,
    Meeting.TermEndDate
FROM meetings.Meetings AS [Meeting]
GO