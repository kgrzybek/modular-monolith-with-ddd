using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail
{
    internal class SendMeetingAttendeeAddedEmailCommand : InternalCommandBase
    {
        internal MemberId AttendeeId { get; }

        internal MeetingId MeetingId { get; }

        [JsonConstructor]
        internal SendMeetingAttendeeAddedEmailCommand(Guid id, MemberId attendeeId, MeetingId meetingId)
            : base(id)
        {
            AttendeeId = attendeeId;
            MeetingId = meetingId;
        }
    }
}