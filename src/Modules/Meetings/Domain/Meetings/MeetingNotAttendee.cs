using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingNotAttendee : Entity
    {
        internal MemberId MemberId { get; private set; }

        internal MeetingId MeetingId { get; private set; }

        private DateTime _decisionDate;

        private bool _decisionChanged;

        private DateTime? _decisionChangeDate;

        private MeetingNotAttendee()
        {
            
        }

        internal MeetingNotAttendee(MeetingId meetingId, MemberId memberId)
        {
            this.MemberId = memberId;
            this.MeetingId = meetingId;
            _decisionDate = DateTime.UtcNow;

            this.AddDomainEvent(new MeetingNotAttendeeAddedDomainEvent(this.MeetingId, this.MemberId));
        }

        internal bool IsActiveNotAttendee(MemberId memberId)
        {
            return !this._decisionChanged && this.MemberId == memberId;
        }

        internal void ChangeDecision()
        {
            _decisionChanged = true;
            _decisionChangeDate = DateTime.UtcNow;
        }
    }
}