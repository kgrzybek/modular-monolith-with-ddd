using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public class MeetingGroupPaymentId : TypedIdValueBase
    {
        public MeetingGroupPaymentId(Guid value) : base(value)
        {
        }
    }
}