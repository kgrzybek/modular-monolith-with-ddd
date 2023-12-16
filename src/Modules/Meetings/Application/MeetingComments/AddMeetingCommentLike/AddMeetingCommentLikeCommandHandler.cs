using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike
{
    internal class AddMeetingCommentLikeCommandHandler : ICommandHandler<AddMeetingCommentLikeCommand>
    {
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMeetingMemberCommentLikesRepository _meetingMemberCommentLikesRepository;
        private readonly IMemberContext _memberContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AddMeetingCommentLikeCommandHandler(
            IMeetingCommentRepository meetingCommentRepository,
            IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository,
            IMemberContext memberContext,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _memberContext = memberContext;
            _sqlConnectionFactory = sqlConnectionFactory;
            _meetingMemberCommentLikesRepository = meetingMemberCommentLikesRepository;
        }

        public async Task Handle(AddMeetingCommentLikeCommand request, CancellationToken cancellationToken)
        {
            var meetingComment = await _meetingCommentRepository.GetByIdAsync(new MeetingCommentId(request.MeetingCommentId));
            if (meetingComment == null)
            {
                throw new InvalidCommandException(new List<string> { "To add like the comment must exist." });
            }

            var connection = _sqlConnectionFactory.GetOpenConnection();
            var likerMeetingGroupMemberData = await MembersQueryHelper.GetMeetingGroupMember(_memberContext.MemberId, meetingComment.GetMeetingId(), connection);

            var meetingMemeberCommentLikesCount = await _meetingMemberCommentLikesRepository.CountMemberCommentLikesAsync(
                    _memberContext.MemberId,
                    new MeetingCommentId(request.MeetingCommentId));

            var like = meetingComment.Like(_memberContext.MemberId, likerMeetingGroupMemberData, meetingMemeberCommentLikesCount);

            await _meetingMemberCommentLikesRepository.AddAsync(like);
        }
    }
}