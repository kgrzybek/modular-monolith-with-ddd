CREATE TABLE meetings.Meetings
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [MeetingGroupId] UNIQUEIDENTIFIER NOT NULL,
    [CreatorId] UNIQUEIDENTIFIER NOT NULL,
    [CreateDate] DATETIME2 NOT NULL,
    [Title] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(4000) NOT NULL,
    [TermStartDate] DATETIME NOT NULL,
    [TermEndDate] DATETIME NOT NULL,
    [LocationName] NVARCHAR(200) NOT NULL,
    [LocationAddress] NVARCHAR(200) NOT NULL,
    [LocationPostalCode] NVARCHAR(200) NULL,
    [LocationCity] NVARCHAR(50) NOT NULL,
    [AttendeesLimit] INT NULL,
    [GuestsLimit] INT NOT NULL,
    [RSVPTermStartDate] DATETIME NULL,
    [RSVPTermEndDate] DATETIME NULL,
    [EventFeeValue] DECIMAL(5, 0) NULL,
    [EventFeeCurrency] VARCHAR(3) NULL,
	[ChangeDate] DATETIME2 NULL,
	[ChangeMemberId] UNIQUEIDENTIFIER NULL,
	[CancelDate] DATETIME NULL,
	[CancelMemberId] UNIQUEIDENTIFIER NULL,
	[IsCanceled] BIT NOT NULL,
	CONSTRAINT [PK_meetings_Meetings_Id] PRIMARY KEY ([Id] ASC)
)
GO