using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike
{
    public class UnlikeMeetingCommentCommandHandler : ICommandHandler<UnlikeMeetingCommentCommand, Unit>
    {
        public Task<Unit> Handle(UnlikeMeetingCommentCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}