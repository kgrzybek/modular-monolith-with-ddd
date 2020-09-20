using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;
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
            Assert.That(meetingCommentingConfigurationCreatedEvent.MeetingId, Is.EqualTo(meeting.Meeting.Id));
            Assert.That(meetingCommentingConfigurationCreatedEvent.IsEnabled, Is.True);
        }
    }
}