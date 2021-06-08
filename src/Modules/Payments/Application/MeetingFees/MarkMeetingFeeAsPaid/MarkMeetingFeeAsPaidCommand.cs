using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid
{
    public class MarkMeetingFeeAsPaidCommand : InternalCommandBase
    {
        [JsonConstructor]
        public MarkMeetingFeeAsPaidCommand(Guid meetingFeeId)
        {
            MeetingFeeId = meetingFeeId;
        }

        public Guid MeetingFeeId { get; }
    }
}