using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations
{
    public class MeetingCommentingConfigurationId : TypedIdValueBase
    {
        public MeetingCommentingConfigurationId(Guid value)
            : base(value)
        {
        }
    }
}