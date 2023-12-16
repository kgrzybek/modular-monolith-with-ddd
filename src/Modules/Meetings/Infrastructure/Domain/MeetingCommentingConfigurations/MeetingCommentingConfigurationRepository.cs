using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingCommentingConfigurations
{
    public class MeetingCommentingConfigurationRepository : IMeetingCommentingConfigurationRepository
    {
        private readonly MeetingsContext _meetingsContext;

        public MeetingCommentingConfigurationRepository(MeetingsContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public async Task AddAsync(MeetingCommentingConfiguration meetingCommentingConfiguration)
        {
            await _meetingsContext.MeetingCommentingConfigurations.AddAsync(meetingCommentingConfiguration);
        }

        public async Task<MeetingCommentingConfiguration> GetByMeetingIdAsync(MeetingId meetingId)
        {
            return await _meetingsContext.MeetingCommentingConfigurations.SingleOrDefaultAsync(c =>
                EF.Property<MeetingId>(c, "_meetingId") == meetingId);
        }
    }
}