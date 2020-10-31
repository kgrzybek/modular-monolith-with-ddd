CREATE VIEW [meetings].[v_MemberMeetings]
AS
SELECT
	[Meeting].[Id],
    [Meeting].[Title],
    [Meeting].[Description],
    [Meeting].[LocationAddress],
    [Meeting].[LocationCity],
    [Meeting].[LocationPostalCode],
    [Meeting].[TermStartDate],
    [Meeting].[TermEndDate],

    [MeetingAttendee].[AttendeeId],
    [MeetingAttendee].[IsRemoved],
    [MeetingAttendee].[RoleCode]
FROM [meetings].[Meetings] AS [Meeting]
    INNER JOIN [meetings].[MeetingAttendees] AS [MeetingAttendee]
        ON [Meeting].[Id] = [MeetingAttendee].[MeetingId]
GO