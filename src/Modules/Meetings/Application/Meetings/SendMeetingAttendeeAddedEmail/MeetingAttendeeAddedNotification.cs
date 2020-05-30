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
        public MeetingId MeetingId { get; }

        public MemberId AttendeeId { get; }

        public MoneyValue Fee { get; }

        [JsonConstructor]
        public MeetingAttendeeAddedNotification(MeetingAttendeeAddedDomainEvent domainEvent, Guid id) : base(domainEvent, id)
        {
            Fee = domainEvent.Fee;
            this.MeetingId = domainEvent.MeetingId;
            this.AttendeeId = domainEvent.AttendeeId;
        }
    }
}