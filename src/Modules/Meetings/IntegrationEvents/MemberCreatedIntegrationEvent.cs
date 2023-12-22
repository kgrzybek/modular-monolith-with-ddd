using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents
{
    public class MemberCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid MemberId { get; }

        public MemberCreatedIntegrationEvent(Guid id, DateTime occurredOn, Guid memberId)
            : base(id, occurredOn)
        {
            MemberId = memberId;
        }
    }
}