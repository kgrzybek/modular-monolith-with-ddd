CREATE VIEW [meetings].[v_MemberMeetingGroups]
AS
SELECT
    [MeetingGroup].Id,
    [MeetingGroup].[Name],
    [MeetingGroup].[Description],
    [MeetingGroup].[LocationCountryCode],
    [MeetingGroup].[LocationCity],

    [MeetingGroupMember].[MemberId],
    [MeetingGroupMember].[RoleCode],
    [MeetingGroupMember].[IsActive],
    [MeetingGroupMember].[JoinedDate]
FROM meetings.MeetingGroups AS [MeetingGroup]
    INNER JOIN [meetings].[MeetingGroupMembers] AS [MeetingGroupMember]
        ON [MeetingGroup].[Id] = [MeetingGroupMember].[MeetingGroupId]
GO
