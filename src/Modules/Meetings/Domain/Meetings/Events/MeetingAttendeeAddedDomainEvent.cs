using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingAttendeeAddedDomainEvent : DomainEventBase
    {
        public MeetingAttendeeAddedDomainEvent(
            MeetingId meetingId,
            MemberId attendeeId,
            DateTime rsvpDate,
            string role,
            int guestsNumber,
            decimal? feeValue,
            string feeCurrency)
        {
            MeetingId = meetingId;
            AttendeeId = attendeeId;
            RSVPDate = rsvpDate;
            Role = role;
            GuestsNumber = guestsNumber;
            FeeValue = feeValue;
            FeeCurrency = feeCurrency;
        }

        public MeetingId MeetingId { get; }

        public MemberId AttendeeId { get; }

        public DateTime RSVPDate { get; }

        public string Role { get; }

        public int GuestsNumber { get; }

        public decimal? FeeValue { get; }

        public string FeeCurrency { get; }
    }
}