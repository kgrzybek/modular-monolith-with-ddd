using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroupProposals
{
    internal class MeetingGroupProposalRepository : IMeetingGroupProposalRepository
    {
        private readonly MeetingsContext _context;

        internal MeetingGroupProposalRepository(MeetingsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MeetingGroupProposal meetingGroupProposal)
        {
            await _context.MeetingGroupProposals.AddAsync(meetingGroupProposal);
        }

        public async Task<MeetingGroupProposal> GetByIdAsync(MeetingGroupProposalId meetingGroupProposalId)
        {
            return await _context.MeetingGroupProposals.FirstOrDefaultAsync(x => x.Id == meetingGroupProposalId);
        }
    }
}
