using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes
{
    public class MeetingMemberCommentLike : Entity, IAggregateRoot
    {
        public MeetingMemberCommentLikeId Id { get; }

        private MeetingCommentId _meetingCommentId;

        private MemberId _memberId;

        private MeetingMemberCommentLike()
        {
            // Only for EF.
        }

        private MeetingMemberCommentLike(MeetingCommentId meetingCommentId, MemberId memberId)
        {
            Id = new MeetingMemberCommentLikeId(Guid.NewGuid());
            _meetingCommentId = meetingCommentId;
            _memberId = memberId;

            this.AddDomainEvent(new MeetingCommentLikedDomainEvent(meetingCommentId, memberId));
        }

        public void Remove()
        {
            this.AddDomainEvent(new MeetingCommentUnlikedDomainEvent(_meetingCommentId, _memberId));
        }

        public static MeetingMemberCommentLike Create(MeetingCommentId meetingCommentId, MemberId memberId)
            => new MeetingMemberCommentLike(meetingCommentId, memberId);
    }
}