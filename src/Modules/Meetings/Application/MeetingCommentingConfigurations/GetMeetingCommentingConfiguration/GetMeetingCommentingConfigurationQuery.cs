using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration
{
    public class GetMeetingCommentingConfigurationQuery : QueryBase<MeetingCommentingConfigurationDto>
    {
        public Guid MeetingId { get; }

        public GetMeetingCommentingConfigurationQuery(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}