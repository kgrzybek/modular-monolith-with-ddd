using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingAttendee : Entity
    {
        internal MemberId AttendeeId { get; private set; }

        internal MeetingId MeetingId { get; private set; }

        private DateTime _decisionDate;

        private MeetingAttendeeRole _role;

        private int _guestsNumber;

        private bool _decisionChanged;

        private DateTime? _decisionChangeDate;

        private DateTime? _removedDate;

        private MemberId _removingMemberId;

        private string _removingReason;

        private bool _isRemoved;

        private MeetingAttendee()
        {

        }

        internal MeetingAttendee(
            MeetingId meetingId, 
            MemberId attendeeId, 
            DateTime decisionDate, 
            MeetingAttendeeRole role, 
            int guestsNumber,
            MoneyValue eventFee)
        {
            this.AttendeeId = attendeeId;
            this.MeetingId = meetingId;
            this._decisionDate = decisionDate;
            this._role = role;
            _guestsNumber = guestsNumber;
            _decisionChanged = false;

            MoneyValue fee;

            if (eventFee != MoneyValue.Zero)
            {
                fee = (1 + guestsNumber) * eventFee;
            }
            else
            {
                fee = MoneyValue.Zero;
            }

            this.AddDomainEvent(new MeetingAttendeeAddedDomainEvent(this.MeetingId, AttendeeId, decisionDate, role, guestsNumber, fee));
        }

        internal void ChangeDecision()
        {
            _decisionChanged = true;
            _decisionChangeDate = DateTime.UtcNow;

            this.AddDomainEvent(new MeetingAttendeeChangedDecisionDomainEvent(this.AttendeeId, this.MeetingId));
        }

        internal bool IsActiveAttendee(MemberId attendeeId)
        {
            return this.AttendeeId == attendeeId && !_decisionChanged;
        }

        internal bool IsActive()
        {
            return !_decisionChangeDate.HasValue && !_isRemoved;
        }

        internal bool IsActiveHost()
        {
            return this.IsActive() && _role == MeetingAttendeeRole.Host;

        }

        internal int GetAttendeeWithGuestsNumber()
        {
            return 1 + _guestsNumber;
        }

        internal void SetAsHost()
        {
            _role = MeetingAttendeeRole.Host;
        }

        internal void SetAsAttendee()
        {
            _role = MeetingAttendeeRole.Attendee;
        }

        internal void Remove(MemberId removingMemberId, string reason)
        {
            this.CheckRule(new ReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule(reason));

            _isRemoved = true;
            _removedDate = DateTime.UtcNow;
            _removingReason = reason;
            _removingMemberId = removingMemberId;
        }
    }
}