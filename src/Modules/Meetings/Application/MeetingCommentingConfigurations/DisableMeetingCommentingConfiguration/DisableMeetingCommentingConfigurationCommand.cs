using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.DisableMeetingCommentingConfiguration
{
    public class DisableMeetingCommentingConfigurationCommand : CommandBase
    {
        public Guid MeetingId { get; }

        public DisableMeetingCommentingConfigurationCommand(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}