namespace CompanyName.MyMeetings.BuildingBlocks.Application.Outbox
{
    /// <summary>
    /// Represents an outbox message.
    /// </summary>
    public class OutboxMessage
    {
        /// <summary>
        /// Gets or sets the unique identifier of the message.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the message occurred.
        /// </summary>
        public DateTime OccurredOn { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the data of the message.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the message was processed.
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboxMessage"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the message.</param>
        /// <param name="occurredOn">The date and time when the message occurred.</param>
        /// <param name="type">The type of the message.</param>
        /// <param name="data">The data of the message.</param>
        public OutboxMessage(Guid id, DateTime occurredOn, string type, string data)
        {
            this.Id = id;
            this.OccurredOn = occurredOn;
            this.Type = type;
            this.Data = data;
        }

        private OutboxMessage()
        {
        }
    }
}