using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members
{
    public interface IMemberRepository
    {
        Task AddAsync(Member member);

        Task<Member> GetByIdAsync(MemberId memberId);
    }
}