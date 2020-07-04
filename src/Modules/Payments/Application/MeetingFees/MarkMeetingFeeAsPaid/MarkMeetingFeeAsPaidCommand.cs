using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees
{
    public class MarkMeetingFeeAsPaidCommand : CommandBase
    {
        public MarkMeetingFeeAsPaidCommand(Guid meetingFeeId)
        {
            MeetingFeeId = meetingFeeId;
        }

        public Guid MeetingFeeId { get; }
    }
}