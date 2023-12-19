using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands
{
    /// <summary>
    /// Base class for internal commands.
    /// </summary>
    public abstract class InternalCommandBase : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalCommandBase"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the command.</param>
        protected InternalCommandBase(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the unique identifier of the command.
        /// </summary>
        public Guid Id { get; }
    }
}