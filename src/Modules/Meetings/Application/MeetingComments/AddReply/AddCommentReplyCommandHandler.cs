using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddReply
{
    public class AddCommentReplyCommandHandler : ICommandHandler<AddCommentReplyCommand, Guid>
    {
        public Task<Guid> Handle(AddCommentReplyCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}