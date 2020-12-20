using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members
{
    public class MeetingGroupMemberDto
    {
        public Guid MeetingGroupId { get; set; }

        public Guid MemberId { get; set; }
    }
}