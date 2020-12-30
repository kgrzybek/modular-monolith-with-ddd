CREATE TABLE [meetings].[MeetingMemberCommentLikes]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [MemberId]  UNIQUEIDENTIFIER NOT NULL,
    [MeetingCommentId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_meetings_MeetingMemberCommentLikes_Id] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_meetings_MeetingMemberCommentLikes_Members] FOREIGN KEY ([MemberId]) REFERENCES meetings.Members ([Id]),
    CONSTRAINT [FK_meetings_MeetingMemberCommentLikes_MeetingComments] FOREIGN KEY ([MeetingCommentId]) REFERENCES meetings.MeetingComments ([Id])
)