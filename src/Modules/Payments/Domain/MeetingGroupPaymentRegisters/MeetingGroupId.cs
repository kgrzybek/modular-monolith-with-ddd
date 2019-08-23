using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public class MeetingGroupId : TypedIdValueBase
    {
        public MeetingGroupId(Guid value) : base(value)
        {
        }
    }
}