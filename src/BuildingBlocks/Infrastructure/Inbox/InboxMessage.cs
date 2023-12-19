namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Inbox
{
    /// <summary>
    /// Represents a message stored in the inbox.
    /// </summary>
    public class InboxMessage
    {
        /// <summary>
        /// Gets the identifier of the message.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the date and time when the message occurred.
        /// </summary>
        public DateTime OccurredOn { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the data associated with the message.
        /// The data is stored as a JSON string.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the message was processed.
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InboxMessage"/> class.
        /// </summary>
        /// <param name="occurredOn">The date and time when the message occurred.</param>
        /// <param name="type">The type of the message.</param>
        /// <param name="data">The data associated with the message.</param>
        public InboxMessage(DateTime occurredOn, string type, string data)
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = occurredOn;
            this.Type = type;
            this.Data = data;
        }

        private InboxMessage()
        {
        }
    }
}