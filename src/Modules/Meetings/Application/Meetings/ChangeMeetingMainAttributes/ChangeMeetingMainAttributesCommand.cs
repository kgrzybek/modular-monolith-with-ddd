using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.ChangeMeetingMainAttributes
{
    public class ChangeMeetingMainAttributesCommand : CommandBase
    {
        public ChangeMeetingMainAttributesCommand(
            Guid meetingId,
            string title,
            DateTime termStartDate,
            DateTime termEndDate,
            string description,
            string meetingLocationName,
            string meetingLocationAddress,
            string meetingLocationPostalCode,
            string meetingLocationCity,
            int? attendeesLimit,
            int guestsLimit,
            DateTime? rsvpTermStartDate,
            DateTime? rsvpTermEndDate,
            decimal? eventFeeValue,
            string eventFeeCurrency)
        {
            MeetingId = meetingId;
            Title = title;
            TermStartDate = termStartDate;
            TermEndDate = termEndDate;
            Description = description;
            MeetingLocationName = meetingLocationName;
            MeetingLocationAddress = meetingLocationAddress;
            MeetingLocationPostalCode = meetingLocationPostalCode;
            MeetingLocationCity = meetingLocationCity;
            AttendeesLimit = attendeesLimit;
            GuestsLimit = guestsLimit;
            RSVPTermStartDate = rsvpTermStartDate;
            RSVPTermEndDate = rsvpTermEndDate;
            EventFeeValue = eventFeeValue;
            EventFeeCurrency = eventFeeCurrency;
        }

        public Guid MeetingId { get; }

        public string Title { get; }

        public DateTime TermStartDate { get; }

        public DateTime TermEndDate { get; }

        public string Description { get; }

        public string MeetingLocationName { get; }

        public string MeetingLocationAddress { get; }

        public string MeetingLocationPostalCode { get; }

        public string MeetingLocationCity { get; }

        public int? AttendeesLimit { get; }

        public int GuestsLimit { get; }

        public DateTime? RSVPTermStartDate { get; }

        public DateTime? RSVPTermEndDate { get; }

        public decimal? EventFeeValue { get; }

        public string EventFeeCurrency { get; }
    }
}