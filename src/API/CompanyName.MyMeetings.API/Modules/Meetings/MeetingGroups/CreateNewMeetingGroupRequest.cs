namespace CompanyName.MyMeetings.API.Modules.Meetings.MeetingGroups
{
    public class CreateNewMeetingGroupRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string LocationCity { get; set; }

        public string LocationCountry { get; set; }
    }
}