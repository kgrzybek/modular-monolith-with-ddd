using System;
using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public interface IAggregateStore
    {
        Task Save<T>(T aggregate) where T : AggregateRoot;

        Task<T> Load<T>(AggregateId<T> aggregateId) where T : AggregateRoot;
    }
}