using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public interface IAggregateStore
    {
        Task Save();

        Task<T> Load<T>(AggregateId<T> aggregateId)
            where T : AggregateRoot;

        List<IDomainEvent> GetChanges();

        void AppendChanges<T>(T aggregate)
            where T : AggregateRoot;

        void ClearChanges();
    }
}