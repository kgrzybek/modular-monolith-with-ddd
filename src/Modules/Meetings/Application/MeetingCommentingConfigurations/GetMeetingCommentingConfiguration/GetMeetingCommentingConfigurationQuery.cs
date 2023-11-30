using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.GetMeetingCommentingConfiguration
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