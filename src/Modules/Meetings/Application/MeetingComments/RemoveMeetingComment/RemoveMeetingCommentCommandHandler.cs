using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment
{
    internal class RemoveMeetingCommentCommandHandler : ICommandHandler<RemoveMeetingCommentCommand>
    {
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMemberContext _memberContext;

        internal RemoveMeetingCommentCommandHandler(IMeetingCommentRepository meetingCommentRepository, IMeetingRepository meetingRepository, IMeetingGroupRepository meetingGroupRepository, IMemberContext memberContext)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _meetingRepository = meetingRepository;
            _meetingGroupRepository = meetingGroupRepository;
            _memberContext = memberContext;
        }

        public async Task<Unit> Handle(RemoveMeetingCommentCommand command, CancellationToken cancellationToken)
        {
            var meetingComment = await _meetingCommentRepository.GetByIdAsync(new MeetingCommentId(command.MeetingCommentId));
            if (meetingComment == null)
            {
                throw new InvalidCommandException(new List<string> { "Meeting comment for removing must exist." });
            }

            var meeting = await _meetingRepository.GetByIdAsync(meetingComment.GetMeetingId());
            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            meetingComment.Remove(_memberContext.MemberId, meetingGroup, command.Reason);

            return Unit.Value;
        }
    }
}