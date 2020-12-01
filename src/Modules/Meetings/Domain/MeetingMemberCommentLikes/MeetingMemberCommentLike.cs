using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
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

        public MeetingMemberCommentLike(MeetingCommentId meetingCommentId, MemberId memberId)
        {
            Id = new MeetingMemberCommentLikeId(Guid.NewGuid());
            _meetingCommentId = meetingCommentId;
            _memberId = memberId;
        }

        public static MeetingMemberCommentLike Create(MeetingCommentId meetingCommentId, MemberId memberId)
            => new MeetingMemberCommentLike(meetingCommentId, memberId);
    }
}