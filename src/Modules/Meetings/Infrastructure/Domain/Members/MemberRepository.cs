using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members
{
    internal class MemberRepository : IMemberRepository
    {
        private readonly MeetingsContext _meetingsContext;

        internal MemberRepository(MeetingsContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public async Task AddAsync(Member member)
        {
            await _meetingsContext.Members.AddAsync(member);
        }

        public async Task<Member> GetByIdAsync(MemberId memberId)
        {
            return await _meetingsContext.Members.FirstOrDefaultAsync(x => x.Id == memberId);
        }
    }
}