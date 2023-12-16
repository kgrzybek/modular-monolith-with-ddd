using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikers
{
    public class GetMeetingCommentLikersQuery : IQuery<List<MeetingCommentLikerDto>>
    {
        public Guid MeetingCommentId { get; }

        public GetMeetingCommentLikersQuery(Guid meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}