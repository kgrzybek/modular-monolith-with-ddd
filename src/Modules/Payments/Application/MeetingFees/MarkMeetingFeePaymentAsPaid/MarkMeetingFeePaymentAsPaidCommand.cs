using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeePaymentAsPaid
{
    public class MarkMeetingFeePaymentAsPaidCommand : CommandBase<Unit>
    {
        public MarkMeetingFeePaymentAsPaidCommand(Guid meetingFeePaymentId)
        {
            MeetingFeePaymentId = meetingFeePaymentId;
        }

        public Guid MeetingFeePaymentId { get; }
    }
}