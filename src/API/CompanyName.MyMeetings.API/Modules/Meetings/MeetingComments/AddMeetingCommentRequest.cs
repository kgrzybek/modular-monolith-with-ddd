using System;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.API.Modules.Meetings.MeetingComments
{
    public class AddMeetingCommentRequest
    {
        public Guid MeetingId { get; set; }

        public string Comment { get; set; }
    }
}