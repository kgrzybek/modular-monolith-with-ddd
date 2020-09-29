using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.EnableMeetingCommentingConfiguration
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