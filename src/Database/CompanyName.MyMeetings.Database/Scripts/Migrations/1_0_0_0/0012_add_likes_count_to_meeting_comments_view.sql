ALTER VIEW [meetings].[v_MeetingComments]
AS
SELECT
    [MeetingComments].Id,
    [MeetingComments].MeetingId,
    [MeetingComments].AuthorId,
    [MeetingComments].InReplyToCommentId,
    [MeetingComments].Comment,
    [MeetingComments].CreateDate,
    [MeetingComments].EditDate,
    [MeetingComments].LikesCount
FROM [meetings].[MeetingComments] AS [MeetingComments]
WHERE [MeetingComments].IsRemoved = 0
GO