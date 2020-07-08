CREATE TABLE meetings.MeetingAttendees
(
	[MeetingId] UNIQUEIDENTIFIER NOT NULL,
	[AttendeeId] UNIQUEIDENTIFIER NOT NULL,
	[DecisionDate] DATETIME2 NOT NULL,
	[RoleCode] VARCHAR(50) NULL,   
    [GuestsNumber] INT NULL,
    [DecisionChanged] BIT NOT NULL,
    [DecisionChangeDate] DATETIME2 NULL,
	[IsRemoved] BIT NOT NULL,
	[RemovingMemberId] UNIQUEIDENTIFIER NULL,
	[RemovingReason] NVARCHAR(500) NULL,
	[RemovedDate] DATETIME2 NULL,
	[BecameNotAttendeeDate] DATETIME2 NULL,
	[FeeValue] DECIMAL(5, 0) NULL,
	[FeeCurrency] VARCHAR(3) NULL,
	[IsFeePaid] BIT NOT NULL,
	CONSTRAINT [PK_meetings_MeetingAttendees_Id] PRIMARY KEY ([MeetingId] ASC, [AttendeeId] ASC, [DecisionDate] ASC)
)
GO