using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.UpdatePaymentInfo
{
    internal class UpdateMeetingGroupPaymentInfoCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal UpdateMeetingGroupPaymentInfoCommand(Guid id, Guid meetingGroupId, DateTime dateTo) : base(id)
        {
            MeetingGroupId = meetingGroupId;
            DateTo = dateTo;
        }

        internal Guid MeetingGroupId { get; }

        internal DateTime DateTo { get; }
    }
}