CREATE TABLE meetings.MeetingNotAttendees
(
	[MeetingId] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
    [DecisionDate] DATETIME2 NOT NULL,
    [DecisionChanged] BIT NOT NULL,
    [DecisionChangeDate] DATETIME2 NULL,
	CONSTRAINT [PK_meetings_MeetingNotAttendees_Id] PRIMARY KEY ([MeetingId] ASC, [MemberId] ASC, [DecisionDate] ASC)
)
GO