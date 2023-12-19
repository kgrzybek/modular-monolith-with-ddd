namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox
{
    /// <summary>
    /// Represents a data transfer object for an outbox message.
    /// </summary>
    public class OutboxMessageDto
    {
        /// <summary>
        /// Gets or sets the identifier of the outbox message.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the message type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the message data.
        /// </summary>
        public string Data { get; set; }
    }
}