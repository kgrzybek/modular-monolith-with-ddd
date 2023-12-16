using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees
{
    public class MeetingFee : AggregateRoot
    {
        private PayerId _payerId;

        private MeetingId _meetingId;

        private MoneyValue _fee;

        private MeetingFeeStatus _status;

        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        private MeetingFee()
        {
        }

        public static MeetingFee Create(
            PayerId payerId,
            MeetingId meetingId,
            MoneyValue fee)
        {
            var meetingFee = new MeetingFee();

            var meetingFeeCreated = new MeetingFeeCreatedDomainEvent(
                Guid.NewGuid(),
                payerId.Value,
                meetingId.Value,
                fee.Value,
                fee.Currency,
                MeetingFeeStatus.WaitingForPayment.Code);

            meetingFee.Apply(meetingFeeCreated);
            meetingFee.AddDomainEvent(meetingFeeCreated);

            return meetingFee;
        }

        public void MarkAsPaid()
        {
            var @event =
                new MeetingFeePaidDomainEvent(
                    this.Id,
                    MeetingFeeStatus.Paid.Code);

            this.Apply(@event);
            this.AddDomainEvent(@event);
        }

        public MeetingFeeSnapshot GetSnapshot()
        {
            return new MeetingFeeSnapshot(this.Id, _payerId.Value, _meetingId.Value);
        }

        private void When(MeetingFeeCreatedDomainEvent meetingFeeCreated)
        {
            this.Id = meetingFeeCreated.MeetingFeeId;
            _payerId = new PayerId(meetingFeeCreated.PayerId);
            _meetingId = new MeetingId(meetingFeeCreated.MeetingId);
            _fee = MoneyValue.Of(meetingFeeCreated.FeeValue, meetingFeeCreated.FeeCurrency);
            _status = MeetingFeeStatus.Of(meetingFeeCreated.Status);
        }

        private void When(MeetingFeeCanceledDomainEvent meetingFeeCanceled)
        {
            _status = MeetingFeeStatus.Of(meetingFeeCanceled.Status);
        }

        private void When(MeetingFeeExpiredDomainEvent meetingFeeExpired)
        {
            _status = MeetingFeeStatus.Of(meetingFeeExpired.Status);
        }

        private void When(MeetingFeePaidDomainEvent meetingFeePaid)
        {
            _status = MeetingFeeStatus.Of(meetingFeePaid.Status);
        }
    }
}