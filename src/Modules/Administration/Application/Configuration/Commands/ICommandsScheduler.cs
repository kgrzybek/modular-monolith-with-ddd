using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands
{
    /// <summary>
    /// Represents a scheduler for enqueueing commands.
    /// </summary>
    public interface ICommandsScheduler
    {
        /// <summary>
        /// Enqueues a command for execution.
        /// </summary>
        /// <param name="command">The command to enqueue.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task EnqueueAsync(ICommand command);

        /// <summary>
        /// Enqueues a command with a result for execution.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="command">The command to enqueue.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}