using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using MediatR;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid
{
    public class MarkMeetingFeeAsPaidCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public MarkMeetingFeeAsPaidCommand(Guid meetingFeeId)
        {
            MeetingFeeId = meetingFeeId;
        }

        public Guid MeetingFeeId { get; }
    }
}