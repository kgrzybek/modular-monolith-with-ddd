using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingId : TypedIdValueBase
    {
        public MeetingId(Guid value)
            : base(value)
        {
        }
    }
}