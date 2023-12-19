namespace CompanyName.MyMeetings.BuildingBlocks.Application
{
    /// <summary>
    /// Represents an interface for accessing the execution context information.
    /// </summary>
    public interface IExecutionContextAccessor
    {
        /// <summary>
        /// Gets the user ID associated with the execution context.
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Gets the correlation ID associated with the execution context.
        /// </summary>
        Guid CorrelationId { get; }

        /// <summary>
        /// Gets a value indicating whether the execution context is available.
        /// </summary>
        bool IsAvailable { get; }
    }
}