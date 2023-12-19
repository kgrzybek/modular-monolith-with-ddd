namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands
{
    /// <summary>
    /// Represents an internal command.
    /// </summary>
    public class InternalCommand
    {
        /// <summary>
        /// Gets or sets the unique identifier of the internal command.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the internal command.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the internal command.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the date when the internal command was processed.
        /// </summary>
        public DateTime? ProcessedDate { get; set; }
    }
}