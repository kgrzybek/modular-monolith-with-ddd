namespace CompanyName.MyMeetings.BuildingBlocks.Application.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }

        public DateTime OccurredOn { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }

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