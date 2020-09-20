using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations
{
    public class MeetingCommentingConfiguration : Entity, IAggregateRoot
    {
        public MeetingCommentingConfigurationId Id { get; }

        private MeetingId _meetingId;

        private bool _isCommentingEnabled;

        private MeetingCommentingConfiguration(MeetingId meetingId)
        {
            this.Id = new MeetingCommentingConfigurationId(Guid.NewGuid());
            this._meetingId = meetingId;
            this._isCommentingEnabled = true;

            this.AddDomainEvent(new MeetingCommentingConfigurationCreatedDomainEvent(this._meetingId, this._isCommentingEnabled));
        }

        private MeetingCommentingConfiguration()
        {
            // Only for EF.
        }

        internal static MeetingCommentingConfiguration Create(MeetingId meetingId)
            => new MeetingCommentingConfiguration(meetingId);
    }
}