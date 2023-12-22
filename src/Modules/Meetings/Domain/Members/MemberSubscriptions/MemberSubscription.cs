using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions
{
    public class MemberSubscription : Entity, IAggregateRoot
    {
        public MemberSubscriptionId Id { get; private set; }

        private DateTime _expirationDate;

        private MemberSubscription()
        {
            // Only for EF.
        }

        private MemberSubscription(MemberId memberId, DateTime expirationDate)
        {
            this.Id = new MemberSubscriptionId(memberId.Value);
            _expirationDate = expirationDate;

            this.AddDomainEvent(new MemberSubscriptionExpirationDateChangedDomainEvent(memberId, _expirationDate));
        }

        public static MemberSubscription CreateForMember(MemberId memberId, DateTime expirationDate)
        {
            return new MemberSubscription(memberId, expirationDate);
        }

        public void ChangeExpirationDate(DateTime expirationDate)
        {
            _expirationDate = expirationDate;

            this.AddDomainEvent(new MemberSubscriptionExpirationDateChangedDomainEvent(
                new MemberId(this.Id.Value),
                _expirationDate));
        }
    }
}