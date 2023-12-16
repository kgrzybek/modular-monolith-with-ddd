using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment
{
    internal class AddMeetingCommentCommandHandler : ICommandHandler<AddMeetingCommentCommand, Guid>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingCommentRepository _meetingCommentRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMeetingCommentingConfigurationRepository _meetingCommentingConfigurationRepository;
        private readonly IMemberContext _memberContext;

        public AddMeetingCommentCommandHandler(
            IMeetingRepository meetingRepository,
            IMeetingCommentRepository meetingCommentRepository,
            IMeetingGroupRepository meetingGroupRepository,
            IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
            IMemberContext memberContext)
        {
            _meetingGroupRepository = meetingGroupRepository;
            _meetingRepository = meetingRepository;
            _meetingCommentingConfigurationRepository = meetingCommentingConfigurationRepository;
            _meetingCommentRepository = meetingCommentRepository;
            _memberContext = memberContext;
        }

        public async Task<Guid> Handle(AddMeetingCommentCommand command, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(command.MeetingId));
            if (meeting == null)
            {
                throw new InvalidCommandException(["Meeting for adding comment must exist."]);
            }

            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            var meetingCommentingConfiguration = await _meetingCommentingConfigurationRepository.GetByMeetingIdAsync(new MeetingId(command.MeetingId));

            var meetingComment = meeting.AddComment(_memberContext.MemberId, command.Comment, meetingGroup, meetingCommentingConfiguration);

            await _meetingCommentRepository.AddAsync(meetingComment);

            return meetingComment.Id.Value;
        }
    }
}