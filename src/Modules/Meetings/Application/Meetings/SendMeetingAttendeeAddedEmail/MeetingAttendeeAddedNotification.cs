using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail
{
    public class MeetingAttendeeAddedNotification : DomainNotificationBase<MeetingAttendeeAddedDomainEvent>
    {
        [JsonConstructor]
        public MeetingAttendeeAddedNotification(MeetingAttendeeAddedDomainEvent domainEvent, Guid id) : base(domainEvent, id)
        {
        }
    }
}