using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

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

        private MoneyValue _fee;

#pragma warning disable CS0414 // Field is assigned but its value is never used
        private bool _isFeePaid;
#pragma warning restore CS0414 // Field is assigned but its value is never used

        private MeetingAttendee()
        {
        }

        internal static MeetingAttendee CreateNew(
            MeetingId meetingId,
            MemberId attendeeId,
            DateTime decisionDate,
            MeetingAttendeeRole role,
            int guestsNumber,
            MoneyValue eventFee)
        {
            return new MeetingAttendee(meetingId, attendeeId, decisionDate, role, guestsNumber, eventFee);
        }

        private MeetingAttendee(
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
            _isFeePaid = false;

            if (eventFee != MoneyValue.Undefined)
            {
                _fee = (1 + guestsNumber) * eventFee;
            }
            else
            {
                _fee = MoneyValue.Undefined;
            }

            this.AddDomainEvent(new MeetingAttendeeAddedDomainEvent(
                this.MeetingId,
                AttendeeId,
                decisionDate,
                role.Value,
                guestsNumber,
                _fee.Value,
                _fee.Currency));
        }

        internal void ChangeDecision()
        {
            _decisionChanged = true;
            _decisionChangeDate = SystemClock.Now;

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

            this.AddDomainEvent(new NewMeetingHostSetDomainEvent(this.MeetingId, this.AttendeeId));
        }

        internal void SetAsAttendee()
        {
            this.CheckRule(new MemberCannotHaveSetAttendeeRoleMoreThanOnceRule(_role));
            _role = MeetingAttendeeRole.Attendee;

            this.AddDomainEvent(new MemberSetAsAttendeeDomainEvent(this.MeetingId, this.AttendeeId));
        }

        internal void Remove(MemberId removingMemberId, string reason)
        {
            this.CheckRule(new ReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule(reason));

            _isRemoved = true;
            _removedDate = SystemClock.Now;
            _removingReason = reason;
            _removingMemberId = removingMemberId;

            this.AddDomainEvent(new MeetingAttendeeRemovedDomainEvent(this.AttendeeId, this.MeetingId, reason));
        }

        internal void MarkFeeAsPayed()
        {
            _isFeePaid = true;

            this.AddDomainEvent(new MeetingAttendeeFeePaidDomainEvent(this.MeetingId, this.AttendeeId));
        }
    }
}