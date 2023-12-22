using System.Threading.Tasks;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Outbox
{
    public interface IOutbox
    {
        void Add(OutboxMessage message);

        Task Save();
    }
}