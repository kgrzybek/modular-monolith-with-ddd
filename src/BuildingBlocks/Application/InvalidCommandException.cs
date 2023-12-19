using System.Collections.ObjectModel;

namespace CompanyName.MyMeetings.BuildingBlocks.Application
{
    /// <summary>
    /// Represents an exception that is thrown when an invalid command is encountered.
    /// </summary>
    public class InvalidCommandException : Exception
    {
        /// <summary>
        /// Gets the list of errors associated with the invalid command.
        /// </summary>
        public ReadOnlyCollection<string> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException"/> class with the specified list of errors.
        /// </summary>
        /// <param name="errors">The list of errors associated with the invalid command.</param>
        public InvalidCommandException(List<string> errors)
        {
            this.Errors = errors.AsReadOnly();
        }
    }
}
