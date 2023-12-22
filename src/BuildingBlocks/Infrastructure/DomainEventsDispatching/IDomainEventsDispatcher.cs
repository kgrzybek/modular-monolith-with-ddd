using System.Threading.Tasks;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}