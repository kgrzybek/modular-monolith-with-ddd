using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Outbox;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Outbox
{
    internal class OutboxAccessor : IOutbox
    {
        private readonly AdministrationContext _context;

        internal OutboxAccessor(AdministrationContext context)
        {
            _context = context;
        }

        public void Add(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
        }
    }
}