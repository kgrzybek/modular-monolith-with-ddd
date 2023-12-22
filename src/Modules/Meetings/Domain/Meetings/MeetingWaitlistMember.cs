using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingWaitlistMember : Entity
    {
        internal MemberId MemberId { get; private set; }

        internal MeetingId MeetingId { get; private set; }

        internal DateTime SignUpDate { get; private set; }

        private bool _isSignedOff;

        private DateTime? _signOffDate;

        private bool _isMovedToAttendees;

        private DateTime? _movedToAttendeesDate;

        private MeetingWaitlistMember()
        {
        }

        private MeetingWaitlistMember(MeetingId meetingId, MemberId memberId)
        {
            this.MemberId = memberId;
            this.MeetingId = meetingId;
            this.SignUpDate = SystemClock.Now;
            _isMovedToAttendees = false;

            this.AddDomainEvent(new MeetingWaitlistMemberAddedDomainEvent(this.MeetingId, this.MemberId));
        }

        internal static MeetingWaitlistMember CreateNew(MeetingId meetingId, MemberId memberId)
        {
            return new MeetingWaitlistMember(meetingId, memberId);
        }

        internal void MarkIsMovedToAttendees()
        {
            _isMovedToAttendees = true;
            _movedToAttendeesDate = SystemClock.Now;
        }

        internal bool IsActiveOnWaitList(MemberId memberId)
        {
            return this.MemberId == memberId && this.IsActive();
        }

        internal bool IsActive()
        {
            return !_isSignedOff && !_isMovedToAttendees;
        }

        internal void SignOff()
        {
            _isSignedOff = true;
            _signOffDate = SystemClock.Now;

            this.AddDomainEvent(new MemberSignedOffFromMeetingWaitlistDomainEvent(this.MeetingId, this.MemberId));
        }
    }
}