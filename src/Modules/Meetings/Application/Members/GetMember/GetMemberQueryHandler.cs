using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members.GetMember
{
    public class GetMemberQueryHandler : IQueryHandler<GetMemberQuery, MemberDto>
    {
        public Task<MemberDto> Handle(GetMemberQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}