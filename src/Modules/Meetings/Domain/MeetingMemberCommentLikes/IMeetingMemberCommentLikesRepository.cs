﻿using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes
{
    public interface IMeetingMemberCommentLikesRepository
    {
        Task AddAsync(MeetingMemberCommentLike meetingMemberCommentLike);

        Task<MeetingMemberCommentLike> GetAsync(MemberId memberId, MeetingCommentId meetingCommentId);

        Task<int> CountMemberCommentLikesAsync(MemberId memberId, MeetingCommentId meetingCommentId);

        void Remove(MeetingMemberCommentLike meetingMemberCommentLike);
    }
}