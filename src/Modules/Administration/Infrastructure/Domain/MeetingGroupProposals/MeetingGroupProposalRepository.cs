using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Domain.MeetingGroupProposals
{
    internal class MeetingGroupProposalRepository : IMeetingGroupProposalRepository
    {
        private readonly AdministrationContext _context;

        internal MeetingGroupProposalRepository(AdministrationContext context)
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
