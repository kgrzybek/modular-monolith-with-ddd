using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments
{
    public class MeetingCommentLikedNotification : DomainNotificationBase<MeetingCommentLikedDomainEvent>
    {
        public MeetingCommentLikedNotification(MeetingCommentLikedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}