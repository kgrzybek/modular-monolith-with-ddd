CREATE VIEW [meetings].[v_MeetingGroupMembers]
AS
SELECT
    [MeetingGroupMember].MeetingGroupId,
    [MeetingGroupMember].MemberId,
    [MeetingGroupMember].RoleCode
FROM meetings.MeetingGroupMembers AS [MeetingGroupMember]
GO