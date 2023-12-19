using CompanyName.MyMeetings.Modules.Administration.Domain.Members;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Domain.Members
{
    /// <summary>
    /// Represents a repository for managing members in the administration domain.
    /// </summary>
    internal class MemberRepository : IMemberRepository
    {
        private readonly AdministrationContext _administrationContext;

        internal MemberRepository(AdministrationContext meetingsContext)
        {
            _administrationContext = meetingsContext;
        }

        /// <inheritdoc/>
        public async Task AddAsync(Member member)
        {
            await _administrationContext.Members.AddAsync(member);
        }

        /// <inheritdoc/>
        public async Task<Member> GetByIdAsync(MemberId memberId)
        {
            return await _administrationContext.Members.FirstOrDefaultAsync(x => x.Id == memberId);
        }
    }
}