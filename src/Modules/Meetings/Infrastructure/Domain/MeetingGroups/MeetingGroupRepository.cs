using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroups
{
    internal class MeetingGroupRepository : IMeetingGroupRepository
    {
        private readonly MeetingsContext _meetingsContext;

        internal MeetingGroupRepository(MeetingsContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public async Task AddAsync(MeetingGroup meetingGroup)
        {
            await _meetingsContext.MeetingGroups.AddAsync(meetingGroup);
        }

        public async Task<int> Commit()
        {
            return await _meetingsContext.SaveChangesAsync();
        }

        public async Task<MeetingGroup> GetByIdAsync(MeetingGroupId id)
        {
            return await _meetingsContext.MeetingGroups.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
