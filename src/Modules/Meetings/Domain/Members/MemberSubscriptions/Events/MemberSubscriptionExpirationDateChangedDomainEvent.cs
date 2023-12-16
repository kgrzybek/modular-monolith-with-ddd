using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions.Events
{
    public class MemberSubscriptionExpirationDateChangedDomainEvent : DomainEventBase
    {
        public MemberSubscriptionExpirationDateChangedDomainEvent(MemberId memberId, DateTime expirationDate)
        {
            MemberId = memberId;
            ExpirationDate = expirationDate;
        }

        public MemberId MemberId { get; }

        public DateTime ExpirationDate { get; }
    }
}