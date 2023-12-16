using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.EditMeetingGroupGeneralAttributes
{
    public class EditMeetingGroupGeneralAttributesCommand : CommandBase
    {
        public EditMeetingGroupGeneralAttributesCommand(Guid meetingGroupId, string name, string description, string locationCity, string locationCountry)
        {
            MeetingGroupId = meetingGroupId;
            Name = name;
            Description = description;
            LocationCity = locationCity;
            LocationCountry = locationCountry;
        }

        public string LocationCountry { get; }

        internal Guid MeetingGroupId { get; }

        internal string Name { get; }

        internal string Description { get; }

        internal string LocationCity { get; }
    }
}