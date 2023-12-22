using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;

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

        public Task Save()
        {
            // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
            return Task.CompletedTask;
        }
    }
}