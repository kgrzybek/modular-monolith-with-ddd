using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

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

        public void EnableCommenting(MemberId enablingMemberId, MeetingGroup meetingGroup)
        {
            CheckRule(new MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule(enablingMemberId, meetingGroup));

            if (!this._isCommentingEnabled)
            {
                this._isCommentingEnabled = true;
                AddDomainEvent(new MeetingCommentingEnabledDomainEvent(this._meetingId));
            }
        }

        public void DisableCommenting(MemberId disablingMemberId, MeetingGroup meetingGroup)
        {
            CheckRule(new MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule(disablingMemberId, meetingGroup));

            if (this._isCommentingEnabled)
            {
                this._isCommentingEnabled = false;
                AddDomainEvent(new MeetingCommentingDisabledDomainEvent(this._meetingId));
            }
        }

        public bool GetIsCommentingEnabled() => _isCommentingEnabled;

        internal static MeetingCommentingConfiguration Create(MeetingId meetingId)
            => new MeetingCommentingConfiguration(meetingId);
    }
}