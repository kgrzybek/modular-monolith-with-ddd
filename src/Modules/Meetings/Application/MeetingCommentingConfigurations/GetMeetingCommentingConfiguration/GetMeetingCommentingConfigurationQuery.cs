using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

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