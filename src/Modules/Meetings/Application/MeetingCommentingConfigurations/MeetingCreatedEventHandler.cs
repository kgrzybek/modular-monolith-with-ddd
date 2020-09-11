using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration
{
    internal class MeetingCreatedEventHandler : INotificationHandler<MeetingCreatedDomainEvent>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingCommentingConfigurationRepository _meetingCommentingConfigurationRepository;

        public MeetingCreatedEventHandler(
            IMeetingRepository meetingRepository,
            IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository)
        {
            _meetingRepository = meetingRepository;
            _meetingCommentingConfigurationRepository = meetingCommentingConfigurationRepository;
        }

        public async Task Handle(MeetingCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(@event.MeetingId);
            var meetingCommentingConfiguration = meeting.CreateCommentingConfiguration();
            await _meetingCommentingConfigurationRepository.AddAsync(meetingCommentingConfiguration);
        }
    }
}