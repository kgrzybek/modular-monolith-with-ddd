namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    /// <summary>
    /// Represents the interface for the Administration module.
    /// </summary>
    public interface IAdministrationModule
    {
        /// <summary>
        /// Executes a command asynchronously and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <returns>A task representing the asynchronous operation with the result.</returns>
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        /// <summary>
        /// Executes a command asynchronously.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ExecuteCommandAsync(ICommand command);

        /// <summary>
        /// Executes a query asynchronously and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <returns>A task representing the asynchronous operation with the result.</returns>
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}