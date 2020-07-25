CREATE TABLE meetings.MeetingComments
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
    [MeetingId] UNIQUEIDENTIFIER NOT NULL,
    [AuthorId] UNIQUEIDENTIFIER NOT NULL,
    [InReplyToCommentId] UNIQUEIDENTIFIER NULL,
    [Comment] VARCHAR(300) NULL,
    [CreateDate] DATETIME NOT NULL,
	[EditDate] DATE NULL,
	CONSTRAINT [PK_meetings_MeetingComments_Id] PRIMARY KEY ([Id] ASC)
)
GO