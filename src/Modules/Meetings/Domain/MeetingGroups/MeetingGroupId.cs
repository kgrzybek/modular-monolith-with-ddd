using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups
{
    public class MeetingGroupId : TypedIdValueBase
    {
        public MeetingGroupId(Guid value)
            : base(value)
        {
        }
    }
}