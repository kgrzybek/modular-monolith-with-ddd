using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.DisbaleMeetingCommentingConfiguration
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