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

        public AddMeetingCommentLikeCommandHandler(
            IMeetingCommentRepository meetingCommentRepository,
            IMeetingGroupRepository meetingGroupRepository,
            IMeetingRepository meetingRepository,
            IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository,
            IMemberContext memberContext)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _meetingGroupRepository = meetingGroupRepository;
            _meetingRepository = meetingRepository;
            _memberContext = memberContext;
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

            var meetingMemeberCommentLikesCount = await _meetingMemberCommentLikesRepository.CountMemberCommentLikesAsync(
                    _memberContext.MemberId,
                    new MeetingCommentId(request.MeetingCommentId));

            var like = meetingComment.Like(meetingGroup, _memberContext.MemberId, meetingMemeberCommentLikesCount);

            await _meetingMemberCommentLikesRepository.AddAsync(like);

            return Unit.Value;
        }
    }
}