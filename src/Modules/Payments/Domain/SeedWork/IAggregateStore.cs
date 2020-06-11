using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public interface IAggregateStore
    {
        Task Save<T>(T aggregate) where T : AggregateRoot;
    }
}