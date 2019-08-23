CREATE TABLE [administration].OutboxMessages
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[OccurredOn] DATETIME2 NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2 NULL,
	CONSTRAINT [PK_administration_OutboxMessages_Id] PRIMARY KEY ([Id] ASC)
)
GO