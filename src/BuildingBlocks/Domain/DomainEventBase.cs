using System;

namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    public class DomainEventBase : IDomainEvent
    {
        public DateTime OccurredOn { get; }

        public DomainEventBase()
        {
            this.OccurredOn = DateTime.UtcNow;
        }
    }
}