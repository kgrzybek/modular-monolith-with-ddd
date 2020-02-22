using System;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments.GetMeetingPayment
{
    public class MeetingPaymentDto
    {
        public Guid PayerId { get; set; }
        public Guid MeetingId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal FeeValue { get; set; }
        public string FeeCurrency { get; set; }
    }
}