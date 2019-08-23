CREATE TABLE meetings.MeetingGroupMembers
(
	[MeetingGroupId] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
	[JoinedDate] DATETIME2 NOT NULL,
	[RoleCode] VARCHAR(50) NOT NULL,   
    [IsActive] BIT NOT NULL,
    [LeaveDate] DATETIME NULL,
	CONSTRAINT [PK_meetings_MeetingGroupMembers_MeetingGroupId_MemberId_JoinedDate] PRIMARY KEY ([MeetingGroupId] ASC, [MemberId] ASC, [JoinedDate] ASC)
)
GO