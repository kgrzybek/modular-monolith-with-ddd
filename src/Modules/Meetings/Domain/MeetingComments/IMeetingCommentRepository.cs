﻿using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments
{
    public interface IMeetingCommentRepository
    {
        Task AddAsync(MeetingComment meetingComment);

        Task<MeetingComment> GetByIdAsync(MeetingCommentId meetingCommentId);
    }
}