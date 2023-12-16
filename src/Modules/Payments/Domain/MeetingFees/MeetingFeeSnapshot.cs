namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees
{
    public class MeetingFeeSnapshot
    {
        public MeetingFeeSnapshot(Guid meetingFeeId, Guid payerId, Guid meetingId)
        {
            MeetingFeeId = meetingFeeId;
            PayerId = payerId;
            MeetingId = meetingId;
        }

        public Guid MeetingFeeId { get; }

        public Guid PayerId { get; }

        public Guid MeetingId { get; }
    }
}