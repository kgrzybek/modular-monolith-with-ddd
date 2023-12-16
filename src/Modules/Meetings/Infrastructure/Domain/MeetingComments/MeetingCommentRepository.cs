using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingComments
{
    public class MeetingCommentRepository : IMeetingCommentRepository
    {
        private readonly MeetingsContext _meetingsContext;

        public MeetingCommentRepository(MeetingsContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public async Task AddAsync(MeetingComment meetingComment)
        {
            await _meetingsContext.MeetingComments.AddAsync(meetingComment);
        }

        public async Task<MeetingComment> GetByIdAsync(MeetingCommentId meetingCommentId)
        {
            return await _meetingsContext.MeetingComments.FindAsync(meetingCommentId);
        }
    }
}