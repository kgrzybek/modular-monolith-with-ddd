﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail
{
    internal class MeetingAttendeeAddedPublishEventNotificationHandler : INotificationHandler<MeetingAttendeeAddedNotification>
    {
        private readonly IEventsBus _eventsBus;

        internal MeetingAttendeeAddedPublishEventNotificationHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public Task Handle(MeetingAttendeeAddedNotification notification, CancellationToken cancellationToken)
        {
            _eventsBus.Publish(new MeetingAttendeeAddedIntegrationEvent(
                Guid.NewGuid(),
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.MeetingId.Value,
                notification.DomainEvent.AttendeeId.Value,
                notification.DomainEvent.FeeValue,
                notification.DomainEvent.FeeCurrency));

            return Task.CompletedTask;
        }
    }
}