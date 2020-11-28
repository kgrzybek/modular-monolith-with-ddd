using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikes
{
    public class GetMeetingCommentLikersQueryHandler : IQueryHandler<GetMeetingCommentLikersQuery, List<MeetingCommentLikerDto>>
    {
        public Task<List<MeetingCommentLikerDto>> Handle(GetMeetingCommentLikersQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}