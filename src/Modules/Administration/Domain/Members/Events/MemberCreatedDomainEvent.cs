using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.Members.Events
{
    public class MemberCreatedDomainEvent : DomainEventBase
    {
        public MemberId MemberId { get; }
        public MemberCreatedDomainEvent(MemberId memberId)
        {
            MemberId = memberId;
        }
    }
}