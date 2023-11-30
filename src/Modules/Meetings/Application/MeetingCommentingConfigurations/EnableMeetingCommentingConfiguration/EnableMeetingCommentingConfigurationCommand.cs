using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;

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