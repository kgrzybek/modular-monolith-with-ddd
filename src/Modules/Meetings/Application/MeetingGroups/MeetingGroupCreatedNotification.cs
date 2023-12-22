using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups
{
    public class MeetingGroupCreatedNotification : DomainNotificationBase<MeetingGroupCreatedDomainEvent>
    {
        [JsonConstructor]
        internal MeetingGroupCreatedNotification(MeetingGroupCreatedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}