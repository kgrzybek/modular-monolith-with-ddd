using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments
{
    public class MeetingFeePayment : AggregateRoot
    {
        private MeetingFeeId _meetingFeeId;

        private MeetingFeePaymentStatus _status;

        public static MeetingFeePayment Create(
            MeetingFeeId meetingFeeId)
        {
            var meetingFeePayment = new MeetingFeePayment();

            var meetingFeePaymentCreated = new MeetingFeePaymentCreatedDomainEvent(
                Guid.NewGuid(),
                meetingFeeId.Value,
                MeetingFeePaymentStatus.WaitingForPayment.Code);

            meetingFeePayment.Apply(meetingFeePaymentCreated);
            meetingFeePayment.AddDomainEvent(meetingFeePaymentCreated);

            return meetingFeePayment;
        }

        public void Expire()
        {
            MeetingFeePaymentPaidDomainEvent @event =
                new MeetingFeePaymentPaidDomainEvent(
                    this.Id,
                    MeetingFeePaymentStatus.Expired.Code);

            this.Apply(@event);
            this.AddDomainEvent(@event);
        }

        public void MarkAsPaid()
        {
            MeetingFeePaymentPaidDomainEvent @event =
                new MeetingFeePaymentPaidDomainEvent(
                    this.Id,
                    MeetingFeePaymentStatus.Paid.Code);

            this.Apply(@event);
            this.AddDomainEvent(@event);
        }

        public MeetingFeePaymentSnapshot GetSnapshot()
        {
            return new MeetingFeePaymentSnapshot(this.Id, _meetingFeeId.Value);
        }

        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        private void When(MeetingFeePaymentCreatedDomainEvent @event)
        {
            this.Id = @event.MeetingFeePaymentId;
            _meetingFeeId = new MeetingFeeId(@event.MeetingFeeId);
            _status = MeetingFeePaymentStatus.Of(@event.Status);
        }

        private void When(MeetingFeePaymentExpiredDomainEvent @event)
        {
            _status = MeetingFeePaymentStatus.Of(@event.Status);
        }

        private void When(MeetingFeePaymentPaidDomainEvent @event)
        {
            _status = MeetingFeePaymentStatus.Of(@event.Status);
        }
    }
}