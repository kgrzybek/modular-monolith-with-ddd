using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands
{
    /// <summary>
    /// Base class for internal commands.
    /// </summary>
    /// <typeparam name="TResult">The type of the command result.</typeparam>
    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalCommandBase{TResult}"/> class.
        /// Generates a new unique identifier for the command.
        /// </summary>
        protected InternalCommandBase()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalCommandBase{TResult}"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier for the command.</param>
        protected InternalCommandBase(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the identifier for the command.
        /// </summary>
        public Guid Id { get; }
    }
}