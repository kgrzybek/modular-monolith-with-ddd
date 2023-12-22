using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class OnlyActiveAttendeeCanBeRemovedFromMeetingRule : IBusinessRule
    {
        private readonly List<MeetingAttendee> _attendees;
        private readonly MemberId _attendeeId;

        internal OnlyActiveAttendeeCanBeRemovedFromMeetingRule(
            List<MeetingAttendee> attendees,
            MemberId attendeeId)
        {
            _attendees = attendees;
            _attendeeId = attendeeId;
        }

        public bool IsBroken() => _attendees.SingleOrDefault(x => x.IsActiveAttendee(_attendeeId)) == null;

        public string Message => "Only active attendee can be removed from meeting";
    }
}