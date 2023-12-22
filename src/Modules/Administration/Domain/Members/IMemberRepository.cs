namespace CompanyName.MyMeetings.Modules.Administration.Domain.Members
{
    public interface IMemberRepository
    {
        Task AddAsync(Member member);

        Task<Member> GetByIdAsync(MemberId memberId);
    }
}