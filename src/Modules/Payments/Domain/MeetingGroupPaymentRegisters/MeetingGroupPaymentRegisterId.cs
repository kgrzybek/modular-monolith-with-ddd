using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public class MeetingGroupPaymentRegisterId : TypedIdValueBase
    {
        public MeetingGroupPaymentRegisterId(Guid value) : base(value)
        {
        }
    }
}