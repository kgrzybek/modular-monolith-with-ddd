using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments
{
    public class MeetingCommentUnlikedNotification : DomainNotificationBase<MeetingCommentUnlikedDomainEvent>
    {
        public MeetingCommentUnlikedNotification(MeetingCommentUnlikedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}