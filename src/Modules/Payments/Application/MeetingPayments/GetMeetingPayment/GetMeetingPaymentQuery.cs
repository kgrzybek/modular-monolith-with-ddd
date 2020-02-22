using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments.GetMeetingPayment
{
    public class GetMeetingPaymentQuery : QueryBase<MeetingPaymentDto>
    {
        public GetMeetingPaymentQuery(Guid meetingId, Guid payerId)
        {
            MeetingId = meetingId;
            PayerId = payerId;
        }

        public Guid MeetingId { get; }
        public Guid PayerId { get; }
    }
}