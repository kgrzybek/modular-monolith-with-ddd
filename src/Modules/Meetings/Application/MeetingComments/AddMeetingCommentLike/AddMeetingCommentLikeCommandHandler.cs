using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike
{
    internal class AddMeetingCommentLikeCommandHandler : ICommandHandler<AddMeetingCommentLikeCommand>
    {
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingMemberCommentLikesRepository _meetingMemberCommentLikesRepository;
        private readonly IMemberContext _memberContext;
        private readonly MeetingCommentService _meetingCommentService;

        public AddMeetingCommentLikeCommandHandler(
            IMeetingCommentRepository meetingCommentRepository,
            IMeetingGroupRepository meetingGroupRepository,
            IMeetingRepository meetingRepository,
            IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository,
            IMemberContext memberContext,
            MeetingCommentService meetingCommentService)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _meetingGroupRepository = meetingGroupRepository;
            _meetingRepository = meetingRepository;
            _memberContext = memberContext;
            _meetingCommentService = meetingCommentService;
            _meetingMemberCommentLikesRepository = meetingMemberCommentLikesRepository;
        }

        public async Task<Unit> Handle(AddMeetingCommentLikeCommand request, CancellationToken cancellationToken)
        {
            var meetingComment = await _meetingCommentRepository.GetByIdAsync(new MeetingCommentId(request.MeetingCommentId));
            if (meetingComment == null)
            {
                throw new InvalidCommandException(new List<string> { "To add like the comment must exist." });
            }

            var meeting = await _meetingRepository.GetByIdAsync(meetingComment.GetMeetingId());

            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            var like = await _meetingCommentService.AddLikeAsync(meetingGroup, meetingComment, _memberContext.MemberId);

            await _meetingMemberCommentLikesRepository.AddAsync(like);

            return Unit.Value;
        }
    }
}