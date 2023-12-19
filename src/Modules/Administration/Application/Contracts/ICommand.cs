using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    /// <summary>
    /// Represents a command that returns a result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the command.</typeparam>
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        /// <summary>
        /// Gets the unique identifier of the command.
        /// </summary>
        Guid Id { get; }
    }

    /// <summary>
    /// Represents a command in the application layer.
    /// </summary>
    public interface ICommand : IRequest
    {
        /// <summary>
        /// Gets the unique identifier of the command.
        /// </summary>
        Guid Id { get; }
    }
}