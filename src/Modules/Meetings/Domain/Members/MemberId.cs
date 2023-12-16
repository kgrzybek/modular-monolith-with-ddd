using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members
{
    public class MemberId : TypedIdValueBase
    {
        public MemberId(Guid value)
            : base(value)
        {
        }
    }
}