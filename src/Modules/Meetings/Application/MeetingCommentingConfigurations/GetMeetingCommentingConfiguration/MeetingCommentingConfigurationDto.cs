namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration
{
    public class MeetingCommentingConfigurationDto
    {
        public Guid MeetingId { get; }

        public bool IsCommentingEnabled { get; }
    }
}