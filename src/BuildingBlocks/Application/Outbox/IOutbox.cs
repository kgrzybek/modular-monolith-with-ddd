namespace CompanyName.MyMeetings.BuildingBlocks.Application.Outbox
{
    /// <summary>
    /// Represents an interface for managing outbox messages.
    /// </summary>
    public interface IOutbox
    {
        /// <summary>
        /// Adds an outbox message to be saved.
        /// </summary>
        /// <param name="message">The outbox message to be added.</param>
        void Add(OutboxMessage message);

        /// <summary>
        /// Saves all the added outbox messages.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task Save();
    }
}