using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate
{
    internal class SetMeetingGroupExpirationDateCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal SetMeetingGroupExpirationDateCommand(Guid id, Guid meetingGroupId, DateTime dateTo)
            : base(id)
        {
            MeetingGroupId = meetingGroupId;
            DateTo = dateTo;
        }

        internal Guid MeetingGroupId { get; }

        internal DateTime DateTo { get; }
    }
}