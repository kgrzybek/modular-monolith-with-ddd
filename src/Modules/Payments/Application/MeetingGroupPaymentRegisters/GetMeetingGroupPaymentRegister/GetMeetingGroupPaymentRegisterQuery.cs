using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.GetMeetingGroupPaymentRegister
{
    public class GetMeetingGroupPaymentRegisterQuery : QueryBase<MeetingGroupPaymentRegisterDto>
    {
        public GetMeetingGroupPaymentRegisterQuery(Guid meetingGroupPaymentRegisterId)
        {
            MeetingGroupPaymentRegisterId = meetingGroupPaymentRegisterId;
        }

        public Guid MeetingGroupPaymentRegisterId { get; }
    }
}