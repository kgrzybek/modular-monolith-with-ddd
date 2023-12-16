using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members.MemberSubscriptions
{
    internal class MemberSubscriptionRepository : IMemberSubscriptionRepository
    {
        private readonly MeetingsContext _meetingsContext;

        internal MemberSubscriptionRepository(MeetingsContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public async Task AddAsync(MemberSubscription member)
        {
            await _meetingsContext.MemberSubscriptions.AddAsync(member);
        }

        public async Task<MemberSubscription> GetByIdOptionalAsync(MemberSubscriptionId memberSubscriptionId)
        {
            return await _meetingsContext.MemberSubscriptions.FirstOrDefaultAsync(x => x.Id == memberSubscriptionId);
        }
    }
}