using System;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.GetMember
{
    public class GetMemberQuery : QueryBase<MemberDto>
    {
        public GetMemberQuery(Guid memberId)
        {
            MemberId = memberId;
        }

        public Guid MemberId { get; }
    }
}