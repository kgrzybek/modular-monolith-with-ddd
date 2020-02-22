using System;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.GetMeetingGroupPayment
{
    public class MeetingGroupPaymentDto
    {
        public Guid Id { get; set; }
        public Guid MeetingGroupPaymentRegisterId { get; set; }
        public DateTime Date { get; set; }
        public DateTime PaymentTermStartDate { get; set; }

        public DateTime PaymentTermEndDate { get; set; }
        public Guid PayerId { get; set; }
    }
}