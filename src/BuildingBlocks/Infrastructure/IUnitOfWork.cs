namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure
{
    /// <summary>
    /// Represents a unit of work for managing database transactions.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits the changes made within the unit of work to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="internalCommandId">The internal command ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the number of affected rows.</returns>
        Task<int> CommitAsync(
            CancellationToken cancellationToken = default,
            Guid? internalCommandId = null);
    }
}