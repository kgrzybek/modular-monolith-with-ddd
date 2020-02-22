using System;
using System.Collections.Generic;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.GetMeetingGroupPayment
{
    public class GetMeetingGroupPaymentsQuery : QueryBase<List<MeetingGroupPaymentDto>>
    {
        public GetMeetingGroupPaymentsQuery(Guid meetingGroupPaymentRegisterId)
        {
            MeetingGroupPaymentRegisterId = meetingGroupPaymentRegisterId;
        }

        public Guid MeetingGroupPaymentRegisterId { get; }
    }
}