using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions
{
    public class MemberSubscriptionExpirationDateChangedNotification : DomainNotificationBase<MemberSubscriptionExpirationDateChangedDomainEvent>
    {
        [JsonConstructor]
        public MemberSubscriptionExpirationDateChangedNotification(MemberSubscriptionExpirationDateChangedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}