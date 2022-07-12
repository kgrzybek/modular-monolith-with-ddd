CREATE TABLE [meetings].[MeetingGroupProposals]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] VARCHAR(200) NULL,
    [LocationCity] NVARCHAR(50) NOT NULL,
    [LocationCountryCode] NVARCHAR(3) NOT NULL,
    [ProposalUserId] UNIQUEIDENTIFIER NOT NULL,
    [ProposalDate] DATETIME NOT NULL,
    [StatusCode] NVARCHAR(50) NOT NULL
	CONSTRAINT [PK_meetings_MeetingGroupProposals_Id] PRIMARY KEY ([Id] ASC)
)
GO