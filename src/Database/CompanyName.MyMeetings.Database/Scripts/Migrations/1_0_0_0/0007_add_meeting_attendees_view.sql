CREATE VIEW [meetings].[v_MeetingAttendees]
AS
SELECT
    [MeetingAttendee].[MeetingId],
    [MeetingAttendee].[AttendeeId],
    [MeetingAttendee].[DecisionDate],
    [MeetingAttendee].[RoleCode],
    [MeetingAttendee].[GuestsNumber],

    [Member].[FirstName],
    [Member].[LastName]
FROM [meetings].[MeetingAttendees] AS [MeetingAttendee]
    INNER JOIN [meetings].[Members] AS [Member]
        ON [MeetingAttendee].[AttendeeId] = [Member].[Id]
GO
