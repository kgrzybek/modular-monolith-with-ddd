using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.SeedWork;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember
{
    public class MemberCreatedNotification : DomainNotificationBase<MeetingCreatedDomainEvent>
    {
        [JsonConstructor]
        public MemberCreatedNotification(MeetingCreatedDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}