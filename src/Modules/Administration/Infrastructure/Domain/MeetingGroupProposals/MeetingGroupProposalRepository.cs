using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Domain.MeetingGroupProposals
{
    /// <summary>
    /// Represents a repository for managing meeting group proposals in the administration domain.
    /// </summary>
    internal class MeetingGroupProposalRepository : IMeetingGroupProposalRepository
    {
        private readonly AdministrationContext _context;

        internal MeetingGroupProposalRepository(AdministrationContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task AddAsync(MeetingGroupProposal meetingGroupProposal)
        {
            await _context.MeetingGroupProposals.AddAsync(meetingGroupProposal);
        }

        /// <inheritdoc/>
        public async Task<MeetingGroupProposal> GetByIdAsync(MeetingGroupProposalId meetingGroupProposalId)
        {
            return await _context.MeetingGroupProposals.FirstOrDefaultAsync(x => x.Id == meetingGroupProposalId);
        }
    }
}
