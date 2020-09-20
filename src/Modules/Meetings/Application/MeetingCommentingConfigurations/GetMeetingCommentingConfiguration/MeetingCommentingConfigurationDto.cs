using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.GetMeetingCommentingConfiguration
{
    public class MeetingCommentingConfigurationDto
    {
        public Guid MeetingId { get; }

        public bool IsCommentingEnabled { get; }
    }
}