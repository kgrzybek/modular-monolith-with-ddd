using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public interface IAggregateStore
    {
        Task Save<T>(T aggregate) where T : AggregateRoot;

        Task<T> Load<T>(AggregateId<T> aggregateId) where T : AggregateRoot;

        List<IDomainEvent> GetChanges();

        void ClearChanges();
    }
}