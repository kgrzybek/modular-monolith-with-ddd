using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Meetings
{
    internal class MeetingRepository : IMeetingRepository
    {
        private readonly MeetingsContext _meetingsContext;

        internal MeetingRepository(MeetingsContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public async Task AddAsync(Meeting meeting)
        {
            await _meetingsContext.Meetings.AddAsync(meeting);
        }

        public async Task<Meeting> GetByIdAsync(MeetingId id)
        {
            return await _meetingsContext.Meetings.FindAsync(id);
        }
    }
}
