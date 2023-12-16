using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly MeetingsContext _meetingsContext;

        internal OutboxAccessor(MeetingsContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public void Add(OutboxMessage message)
        {
            _meetingsContext.OutboxMessages.Add(message);
        }

        public Task Save()
        {
            return Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
        }
    }
}