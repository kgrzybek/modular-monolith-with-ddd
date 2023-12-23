using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentReply
{
    internal class AddReplyToMeetingCommentCommandHandler : ICommandHandler<AddReplyToMeetingCommentCommand, Guid>
    {
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMeetingCommentingConfigurationRepository _meetingCommentingConfigurationRepository;
        private readonly IMemberContext _memberContext;

        internal AddReplyToMeetingCommentCommandHandler(IMeetingCommentRepository meetingCommentRepository, IMeetingRepository meetingRepository, IMeetingGroupRepository meetingGroupRepository, IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository, IMemberContext memberContext)
        {
            _meetingCommentRepository = meetingCommentRepository;
            _meetingRepository = meetingRepository;
            _meetingGroupRepository = meetingGroupRepository;
            _meetingCommentingConfigurationRepository = meetingCommentingConfigurationRepository;
            _memberContext = memberContext;
        }

        public async Task<Guid> Handle(AddReplyToMeetingCommentCommand command, CancellationToken cancellationToken)
        {
            var meetingComment = await _meetingCommentRepository.GetByIdAsync(new MeetingCommentId(command.InReplyToCommentId));
            if (meetingComment == null)
            {
                throw new InvalidCommandException(["To create reply the comment must exist."]);
            }

            var meeting = await _meetingRepository.GetByIdAsync(meetingComment.GetMeetingId());

            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            var meetingCommentingConfiguration = await _meetingCommentingConfigurationRepository.GetByMeetingIdAsync(meetingComment.GetMeetingId());

            var replyToComment = meetingComment.Reply(_memberContext.MemberId, command.Reply, meetingGroup, meetingCommentingConfiguration);
            await _meetingCommentRepository.AddAsync(replyToComment);

            return replyToComment.Id.Value;
        }
    }
}