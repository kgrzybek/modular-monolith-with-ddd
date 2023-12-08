using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.EnableMeetingCommentingConfiguration
{
    internal class EnableMeetingCommentingConfigurationCommandHandler : ICommandHandler<EnableMeetingCommentingConfigurationCommand>
    {
        private readonly IMeetingCommentingConfigurationRepository _meetingCommentingConfigurationRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingGroupRepository _meetingGroupRepository;
        private readonly IMemberContext _memberContext;

        public EnableMeetingCommentingConfigurationCommandHandler(
            IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
            IMeetingRepository meetingRepository,
            IMeetingGroupRepository meetingGroupRepository,
            IMemberContext memberContext)
        {
            _meetingCommentingConfigurationRepository = meetingCommentingConfigurationRepository;
            _meetingRepository = meetingRepository;
            _meetingGroupRepository = meetingGroupRepository;
            _memberContext = memberContext;
        }

        public async Task Handle(EnableMeetingCommentingConfigurationCommand command, CancellationToken cancellationToken)
        {
            var meetingCommentingConfiguration = await _meetingCommentingConfigurationRepository.GetByMeetingIdAsync(new MeetingId(command.MeetingId));
            if (meetingCommentingConfiguration == null)
            {
                throw new InvalidCommandException(new List<string> { "Meeting commenting configuration for enabling commenting must exist." });
            }

            var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(command.MeetingId));

            var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.GetMeetingGroupId());

            meetingCommentingConfiguration.EnableCommenting(_memberContext.MemberId, meetingGroup);
        }
    }
}