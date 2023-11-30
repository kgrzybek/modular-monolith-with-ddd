using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;
using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingDetails
{
    public class GetMeetingDetailsQuery : QueryBase<MeetingDetailsDto>
    {
        public GetMeetingDetailsQuery(Guid meetingId)
        {
            MeetingId = meetingId;
        }

        public Guid MeetingId { get; }
    }
}