using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Outbox
{
    /// <summary>
    /// Represents an implementation of the <see cref="IOutbox"/> interface for accessing the outbox messages in the Administration module.
    /// </summary>
    internal class OutboxAccessor : IOutbox
    {
        private readonly AdministrationContext _context;

        internal OutboxAccessor(AdministrationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new outbox message to the context.
        /// </summary>
        /// <param name="message">The outbox message to add.</param>
        public void Add(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
        }

        /// <summary>
        /// Saves the changes made to the outbox messages.
        /// Save is done automatically using EF Core Change Tracking mechanism during SaveChanges,
        /// using this method is not required.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}