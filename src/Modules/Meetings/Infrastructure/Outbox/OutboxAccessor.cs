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
    }
}