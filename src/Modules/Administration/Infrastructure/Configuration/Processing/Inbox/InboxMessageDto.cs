namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Inbox
{
    /// <summary>
    /// Represents a data transfer object for an inbox message.
    /// </summary>
    public class InboxMessageDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the inbox message.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the inbox message.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the data of the inbox message.
        /// </summary>
        public string Data { get; set; }
    }
}