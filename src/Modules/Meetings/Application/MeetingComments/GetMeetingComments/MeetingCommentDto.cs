namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments
{
    public class MeetingCommentDto
    {
        public Guid Id { get; }

        public Guid? InReplyToCommentId { get; }

        public Guid AuthorId { get; }

        public string Comment { get; }

        public DateTime CreateDate { get; }

        public DateTime? EditDate { get; }

        public int LikesCount { get; }
    }
}