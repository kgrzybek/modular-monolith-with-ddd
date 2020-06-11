using System;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public interface IDomainEvent
    {
        public Guid Id { get; }

        public DateTime OccurredOn { get; }
    }
}