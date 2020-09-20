using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events
{
    public class MeetingCommentingConfigurationCreatedDomainEvent : DomainEventBase
    {
        public MeetingId MeetingId { get; }

        public bool IsEnabled { get; }

        public MeetingCommentingConfigurationCreatedDomainEvent(MeetingId meetingId, bool isEnabled)
        {
            MeetingId = meetingId;
            IsEnabled = isEnabled;
        }
    }
}