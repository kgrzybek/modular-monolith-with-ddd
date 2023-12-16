namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups
{
    public class MeetingGroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LocationCountryCode { get; set; }

        public string LocationCity { get; set; }
    }
}