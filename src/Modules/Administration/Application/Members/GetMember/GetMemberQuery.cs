using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;
using System;

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