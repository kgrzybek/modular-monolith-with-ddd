using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SignOffMemberFromWaitlist
{
    public class SignOffMemberFromWaitlistCommand : CommandBase
    {
        public Guid MeetingId { get; }

        public SignOffMemberFromWaitlistCommand(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}