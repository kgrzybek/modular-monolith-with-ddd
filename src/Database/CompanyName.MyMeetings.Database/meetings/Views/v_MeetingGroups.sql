
CREATE VIEW [meetings].[v_MeetingGroups]
AS
SELECT
    [MeetingGroup].Id,
    [MeetingGroup].[Name],
    [MeetingGroup].[Description],
    [MeetingGroup].[LocationCountryCode],
    [MeetingGroup].[LocationCity]
FROM meetings.MeetingGroups AS [MeetingGroup]
GO