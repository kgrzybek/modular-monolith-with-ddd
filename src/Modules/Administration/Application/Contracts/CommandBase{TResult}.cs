namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    /// <summary>
    /// Base class for commands in the application layer.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the command.</typeparam>
    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase{TResult}"/> class.
        /// Generates a new unique identifier for the command.
        /// </summary>
        protected CommandBase()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase{TResult}"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier for the command.</param>
        protected CommandBase(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the unique identifier for the command.
        /// </summary>
        public Guid Id { get; }
    }
}