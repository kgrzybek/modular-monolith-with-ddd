using System;
using System.Collections.Generic;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public class MeetingGroupPaymentRegister : Entity, IAggregateRoot
    {
        private MeetingGroupPaymentRegister()
        {
            _payments = new List<MeetingGroupPayment>();
        }

        public MeetingGroupPaymentRegisterId Id { get; private set; }

        private DateTime _createDate;

        private List<MeetingGroupPayment> _payments;

        private MeetingGroupPaymentRegister(MeetingGroupId meetingGroupId)
        {
            this.Id = new MeetingGroupPaymentRegisterId(meetingGroupId.Value);
            _createDate = DateTime.UtcNow;

            _payments = new List<MeetingGroupPayment>();

            this.AddDomainEvent(new MeetingGroupPaymentRegisterCreatedDomainEvent(this.Id));
        }

        public static MeetingGroupPaymentRegister CreatePaymentScheduleForMeetingGroup(MeetingGroupId meetingGroupId)
        {
            return new MeetingGroupPaymentRegister(meetingGroupId);
        }

        public void RegisterPayment(PaymentTerm term, PayerId payerId)
        {
            _payments.Add(new MeetingGroupPayment(term, payerId));

            this.AddDomainEvent(new PaymentRegisteredDomainEvent(this.Id, term.EndDate));
        }
    }
}