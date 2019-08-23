using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Outbox;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly PaymentsContext _meetingsContext;

        internal OutboxAccessor(PaymentsContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public void Add(OutboxMessage message)
        {
            _meetingsContext.OutboxMessages.Add(message);
        }
    }
}