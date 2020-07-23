using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class CommentCanBeAddedOnlyByAttendeeRule : IBusinessRule
    {
        private readonly MemberId _authorId;
        private readonly IReadOnlyCollection<MeetingAttendee> _attendees;

        public CommentCanBeAddedOnlyByAttendeeRule(MemberId authorId, IReadOnlyCollection<MeetingAttendee> attendees)
        {
            _authorId = authorId;
            _attendees = attendees;
        }

        public bool IsBroken() => _attendees.SingleOrDefault(a => a.AttendeeId == _authorId) == null;

        public string Message => "Only meeting attendee can add comments";
    }
}