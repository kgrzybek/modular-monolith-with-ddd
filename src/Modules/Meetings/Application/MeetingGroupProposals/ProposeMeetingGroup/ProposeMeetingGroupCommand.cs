using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup
{
    public class ProposeMeetingGroupCommand : CommandBase<Guid>
    {
        public ProposeMeetingGroupCommand(string name, string description, string locationCity, string locationCountryCode)
        {
            Name = name;
            Description = description;
            LocationCity = locationCity;
            LocationCountryCode = locationCountryCode;
        }

        public string Name { get; }

        public string Description { get; }

        public string LocationCity { get; }

        public string LocationCountryCode { get; }
    }
}