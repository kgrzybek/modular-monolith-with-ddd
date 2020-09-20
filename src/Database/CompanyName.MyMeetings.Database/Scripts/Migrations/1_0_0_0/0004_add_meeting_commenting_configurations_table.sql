CREATE TABLE meetings.MeetingCommentingConfigurations
(
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [MeetingId]             UNIQUEIDENTIFIER NOT NULL,
    [IsCommentingEnabled]   BIT              NOT NULL,
    CONSTRAINT [PK_meetings_MeetingCommentingConfigurations_Id] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_meetings_MeetingCommentingConfigurations_Meetings] FOREIGN KEY ([MeetingId]) REFERENCES meetings.Meetings ([Id])
);
GO