using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.EnableMeetingCommentingConfiguration
{
    public class EnableMeetingCommentingConfigurationCommand : CommandBase
    {
        public Guid MeetingId { get; }

        public EnableMeetingCommentingConfigurationCommand(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}