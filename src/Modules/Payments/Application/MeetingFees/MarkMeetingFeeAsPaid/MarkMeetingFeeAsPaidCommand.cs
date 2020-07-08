using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid
{
    public class MarkMeetingFeeAsPaidCommand : InternalCommandBase<Unit>
    {
        public MarkMeetingFeeAsPaidCommand(Guid meetingFeeId)
        {
            MeetingFeeId = meetingFeeId;
        }

        public Guid MeetingFeeId { get; }
    }
}