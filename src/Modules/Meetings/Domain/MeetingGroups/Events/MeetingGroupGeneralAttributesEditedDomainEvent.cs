using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingGroupGeneralAttributesEditedDomainEvent : DomainEventBase
    {
        public string NewName { get; }

        public string NewDescription { get; }

        public MeetingGroupLocation NewLocation { get; }

        public MeetingGroupGeneralAttributesEditedDomainEvent(string newName, string newDescription, MeetingGroupLocation newLocation)
        {
            this.NewName = newName;
            this.NewDescription = newDescription;
            this.NewLocation = newLocation;
        }
    }
}
