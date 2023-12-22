using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees
{
    public class MeetingId : TypedIdValueBase
    {
        public MeetingId(Guid value)
            : base(value)
        {
        }
    }
}