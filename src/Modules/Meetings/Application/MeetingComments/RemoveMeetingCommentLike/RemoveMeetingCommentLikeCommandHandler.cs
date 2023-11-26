using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike
{
    internal class RemoveMeetingCommentLikeCommandHandler : ICommandHandler<RemoveMeetingCommentLikeCommand>
    {
        private readonly IMeetingMemberCommentLikesRepository _meetingMemberCommentLikesRepository;
        private readonly IMemberContext _memberContext;

        internal RemoveMeetingCommentLikeCommandHandler(IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository, IMemberContext memberContext)
        {
            _meetingMemberCommentLikesRepository = meetingMemberCommentLikesRepository;
            _memberContext = memberContext;
        }

        public async Task Handle(RemoveMeetingCommentLikeCommand command, CancellationToken cancellationToken)
        {
            var commentLike = await _meetingMemberCommentLikesRepository.GetAsync(_memberContext.MemberId, new MeetingCommentId(command.MeetingCommentId));
            if (commentLike == null)
            {
                throw new InvalidCommandException(new List<string> { "Meeting comment like for removing must exist." });
            }

            commentLike.Remove();

            _meetingMemberCommentLikesRepository.Remove(commentLike);
        }
    }
}