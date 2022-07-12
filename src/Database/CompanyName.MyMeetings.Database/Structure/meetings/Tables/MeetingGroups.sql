CREATE TABLE meetings.MeetingGroups
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] VARCHAR(200) NULL,
    [LocationCity] NVARCHAR(50) NOT NULL,
    [LocationCountryCode] NVARCHAR(3) NOT NULL,
    [CreatorId] UNIQUEIDENTIFIER NOT NULL,
    [CreateDate] DATETIME NOT NULL,
	[PaymentDateTo] DATE NULL,
	CONSTRAINT [PK_meetings_MeetingGroups_Id] PRIMARY KEY ([Id] ASC)
)
GO