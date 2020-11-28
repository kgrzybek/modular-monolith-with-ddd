using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members.GetMember
{
    public class GetMemberQuery : IQuery<MemberDto>
    {
        public Guid Id { get; }

        public GetMemberQuery(Guid id)
        {
            Id = id;
        }
    }
}