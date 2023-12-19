using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.GetMember
{
    /// <summary>
    /// Represents a query to retrieve a member by their ID.
    /// </summary>
    public class GetMemberQuery(Guid memberId) : QueryBase<MemberDto>
    {
        public Guid MemberId { get; } = memberId;
    }
}