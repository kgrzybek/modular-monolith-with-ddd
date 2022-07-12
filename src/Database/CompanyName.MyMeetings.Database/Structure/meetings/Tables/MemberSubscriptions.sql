CREATE TABLE [meetings].[MemberSubscriptions]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [ExpirationDate] DATETIME NOT NULL,
    CONSTRAINT [PK_meetings_MemberSubscriptions_Id] PRIMARY KEY ([Id] ASC)
)
GO