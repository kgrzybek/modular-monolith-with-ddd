using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents
{
    public class MeetingAttendeeAddedIntegrationEvent : IntegrationEvent
    {
        public Guid MeetingId { get; }

        public Guid AttendeeId { get; }

        public decimal? FeeValue { get; }

        public string FeeCurrency { get; }

        public MeetingAttendeeAddedIntegrationEvent(
            Guid id,
            DateTime occurredOn,
            Guid meetingId,
            Guid attendeeId,
            decimal? feeValue,
            string feeCurrency)
            : base(id, occurredOn)
        {
            MeetingId = meetingId;
            AttendeeId = attendeeId;
            FeeValue = feeValue;
            FeeCurrency = feeCurrency;
        }
    }
}