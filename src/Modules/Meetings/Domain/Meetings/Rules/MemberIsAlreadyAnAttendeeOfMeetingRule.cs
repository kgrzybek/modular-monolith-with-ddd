using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MemberIsAlreadyAnAttendeeOfMeetingRule : IBusinessRule
    {
        private readonly MemberId _attendeeId;

        private readonly List<MeetingAttendee> _attendees;
        public MemberIsAlreadyAnAttendeeOfMeetingRule(MemberId attendeeId, List<MeetingAttendee> attendees)
        {
            this._attendeeId = attendeeId;
            _attendees = attendees;
        }

        public bool IsBroken() => _attendees.SingleOrDefault(x => x.IsActiveAttendee(_attendeeId)) != null;

        public string Message => "Member is already an attendee of this meeting";
    }
}