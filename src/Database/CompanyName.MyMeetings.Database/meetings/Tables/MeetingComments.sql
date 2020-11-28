CREATE TABLE meetings.MeetingComments
(
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [MeetingId]             UNIQUEIDENTIFIER NOT NULL,
    [AuthorId]              UNIQUEIDENTIFIER NOT NULL,
    [InReplyToCommentId]    UNIQUEIDENTIFIER NULL,
    [Comment]               VARCHAR(300)     NULL,
    [IsRemoved]             BIT              NOT NULL,
    [RemovedByReason]       VARCHAR(300)     NULL,
    [CreateDate]            DATETIME         NOT NULL,
    [EditDate]              DATETIME         NULL,
    [LikesCount]            INT              NOT NULL,
    CONSTRAINT [PK_meetings_MeetingComments_Id] PRIMARY KEY ([Id] ASC)
)
GO