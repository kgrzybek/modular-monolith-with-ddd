namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.GetMeetingFees
{
    public class MeetingFeeDto
    {
        public Guid MeetingFeeId { get; }

        public Guid PayerId { get; }

        public Guid MeetingId { get; }

        public decimal FeeValue { get; }

        public string FeeCurrency { get; }

        public string Status { get; }
    }
}