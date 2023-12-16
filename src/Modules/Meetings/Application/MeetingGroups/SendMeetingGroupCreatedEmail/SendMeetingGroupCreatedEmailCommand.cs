using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SendMeetingGroupCreatedEmail
{
    internal class SendMeetingGroupCreatedEmailCommand : InternalCommandBase
    {
        internal MeetingGroupId MeetingGroupId { get; }

        internal MemberId CreatorId { get; }

        [JsonConstructor]
        internal SendMeetingGroupCreatedEmailCommand(Guid id, MeetingGroupId meetingGroupId, MemberId creatorId)
            : base(id)
        {
            MeetingGroupId = meetingGroupId;
            CreatorId = creatorId;
        }
    }
}