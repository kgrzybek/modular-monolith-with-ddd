using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups
{
    internal class MeetingGroupCreatedNotification : DomainNotificationBase<MeetingGroupCreatedDomainEvent>
    {
        internal MeetingGroupId MeetingGroupId { get; }

        internal MemberId CreatorId { get; }

        internal MeetingGroupCreatedNotification(MeetingGroupCreatedDomainEvent domainEvent) : base(domainEvent)
        {
            this.MeetingGroupId = domainEvent.MeetingGroupId;
            this.CreatorId = domainEvent.CreatorId;
        }
        
        [JsonConstructor]
        internal MeetingGroupCreatedNotification(MeetingGroupId meetingGroupId, MemberId creatorId) : base(null)
        {
            this.MeetingGroupId = meetingGroupId;
            this.CreatorId = creatorId;
        }
    }
}