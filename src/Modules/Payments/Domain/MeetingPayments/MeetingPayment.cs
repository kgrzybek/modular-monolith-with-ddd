using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments
{
    public class MeetingPayment : Entity, IAggregateRoot
    {
        public PayerId PayerId { get; private set; }

        public MeetingId MeetingId { get; private set; }

        private MoneyValue _fee;

        private DateTime _createDate;

        private DateTime? _paymentDate;

        private MeetingPayment()
        {
            
        }

        private MeetingPayment(PayerId payerId, MeetingId meetingId, MoneyValue fee)
        {
            PayerId = payerId;
            MeetingId = meetingId;
            _createDate = DateTime.UtcNow;
            _fee = fee;
        }

        public static MeetingPayment CreatePaymentForMeeting(PayerId payerId, MeetingId meetingId, MoneyValue fee)
        {
            return new MeetingPayment(payerId, meetingId, fee);
        }

        public void MarkIsPayed()
        {
            _paymentDate = DateTime.UtcNow;
        }
    }
}