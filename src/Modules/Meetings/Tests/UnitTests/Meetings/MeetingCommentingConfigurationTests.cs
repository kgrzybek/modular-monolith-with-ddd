using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using FluentAssertions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingCommentingConfigurationTests : MeetingTestsBase
    {
        [Test]
        public void CreateMeetingCommentingConfiguration_IsSuccessful()
        {
            // Arrange
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions());

            // Act
            var meetingCommentingConfiguration = meeting.Meeting.CreateCommentingConfiguration();

            // Assert
            var meetingCommentingConfigurationCreatedEvent = AssertPublishedDomainEvent<MeetingCommentingConfigurationCreatedDomainEvent>(meetingCommentingConfiguration);
            meetingCommentingConfigurationCreatedEvent.MeetingId.Should().Be(meeting.Meeting.Id);
            meetingCommentingConfigurationCreatedEvent.IsEnabled.Should().BeTrue();
        }

        [Test]
        public void DisableCommenting_IsSuccessfull()
        {
            // Arrange
            var organizerId = new MemberId(Guid.NewGuid());
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions { CreatorId = organizerId });
            var meetingCommentingConfiguration = meeting.Meeting.CreateCommentingConfiguration();

            meetingCommentingConfiguration.ClearDomainEvents();

            // Act
            meetingCommentingConfiguration.DisableCommenting(organizerId, meeting.MeetingGroup);

            // Assert
            var meetingCommentingDisabledEvent = AssertPublishedDomainEvent<MeetingCommentingDisabledDomainEvent>(meetingCommentingConfiguration);
            meetingCommentingDisabledEvent.MeetingId.Should().Be(meeting.Meeting.Id);
        }

        [Test]
        public void DisableCommenting_WhenMemberIsNotGroupOrganizer_BreakMeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule()
        {
            // Arrange
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions());
            var meetingCommentingConfiguration = meeting.Meeting.CreateCommentingConfiguration();

            AssertBrokenRule<MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule>(() =>
            {
                meetingCommentingConfiguration.DisableCommenting(new MemberId(Guid.NewGuid()), meeting.MeetingGroup);
            });
        }

        [Test]
        public void DisableCommenting_WhenCommentingAlreadyDisabled_IsIgnored()
        {
            // Arrange
            var organizerId = new MemberId(Guid.NewGuid());
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions { CreatorId = organizerId });
            var meetingCommentingConfiguration = meeting.Meeting.CreateCommentingConfiguration();
            meetingCommentingConfiguration.DisableCommenting(organizerId, meeting.MeetingGroup);

            meetingCommentingConfiguration.ClearDomainEvents();

            // Act
            meetingCommentingConfiguration.DisableCommenting(organizerId, meeting.MeetingGroup);

            // Assert
            AssertDomainEventNotPublished<MeetingCommentingDisabledDomainEvent>(meetingCommentingConfiguration);
        }

        [Test]
        public void EnableCommenting_IsSuccessfull()
        {
            // Arrange
            var organizerId = new MemberId(Guid.NewGuid());
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions { CreatorId = organizerId });
            var meetingCommentingConfiguration = meeting.Meeting.CreateCommentingConfiguration();
            meetingCommentingConfiguration.DisableCommenting(organizerId, meeting.MeetingGroup);

            meetingCommentingConfiguration.ClearDomainEvents();

            // Act
            meetingCommentingConfiguration.EnableCommenting(organizerId, meeting.MeetingGroup);

            // Assert
            var meetingCommentingEnabledEvent = AssertPublishedDomainEvent<MeetingCommentingEnabledDomainEvent>(meetingCommentingConfiguration);
            meetingCommentingEnabledEvent.MeetingId.Should().Be(meeting.Meeting.Id);
        }

        [Test]
        public void EnableCommenting_WhenMemberIsNotGroupOrganizer_BreakMeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule()
        {
            // Arrange
            var organizerId = new MemberId(Guid.NewGuid());
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions { CreatorId = organizerId });
            var meetingCommentingConfiguration = meeting.Meeting.CreateCommentingConfiguration();
            meetingCommentingConfiguration.DisableCommenting(organizerId, meeting.MeetingGroup);

            meetingCommentingConfiguration.ClearDomainEvents();

            AssertBrokenRule<MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule>(() =>
            {
                meetingCommentingConfiguration.EnableCommenting(new MemberId(Guid.NewGuid()), meeting.MeetingGroup);
            });
        }

        [Test]
        public void EnableCommenting_WhenCommentingAlreadyEnabled_IsIgnored()
        {
            // Arrange
            var organizerId = new MemberId(Guid.NewGuid());
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions { CreatorId = organizerId });
            var meetingCommentingConfiguration = meeting.Meeting.CreateCommentingConfiguration();

            meetingCommentingConfiguration.ClearDomainEvents();

            // Act
            meetingCommentingConfiguration.EnableCommenting(organizerId, meeting.MeetingGroup);

            // Assert
            AssertDomainEventNotPublished<MeetingCommentingDisabledDomainEvent>(meetingCommentingConfiguration);
        }
    }
}