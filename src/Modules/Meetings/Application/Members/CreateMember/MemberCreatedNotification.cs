using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember
{
    public class MemberCreatedNotification : DomainNotificationBase<MeetingCreatedDomainEvent>
    {
        [JsonConstructor]
        public MemberCreatedNotification(MeetingCreatedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}