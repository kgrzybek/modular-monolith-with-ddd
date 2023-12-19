namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    /// <summary>
    /// Base class for commands in the application layer.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Gets the unique identifier of the command.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class with a new unique identifier.
        /// </summary>
        protected CommandBase()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the command.</param>
        protected CommandBase(Guid id)
        {
            Id = id;
        }
    }
}