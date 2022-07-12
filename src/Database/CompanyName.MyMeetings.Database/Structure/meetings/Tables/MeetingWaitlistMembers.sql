CREATE TABLE meetings.MeetingWaitlistMembers
(
	[MeetingId] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
    [SignUpDate] DATETIME2 NOT NULL,
    [IsSignedOff] BIT NOT NULL,
    [SignOffDate] DATETIME2 NULL,
    [IsMovedToAttendees] BIT NOT NULL,
    [MovedToAttendeesDate] DATETIME2 NULL,
	CONSTRAINT [PK_meetings_MeetingWaitlistMembers_MeetingId_MemberId_SignUpDate] PRIMARY KEY ([MeetingId] ASC, [MemberId] ASC, [SignUpDate] ASC)
)
GO