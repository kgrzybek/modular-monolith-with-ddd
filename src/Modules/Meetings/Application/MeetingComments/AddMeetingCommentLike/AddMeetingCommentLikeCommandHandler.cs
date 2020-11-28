using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike
{
    public class AddMeetingCommentLikeCommandHandler : ICommandHandler<AddMeetingCommentLikeCommand>
    {
        public Task<Unit> Handle(AddMeetingCommentLikeCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}