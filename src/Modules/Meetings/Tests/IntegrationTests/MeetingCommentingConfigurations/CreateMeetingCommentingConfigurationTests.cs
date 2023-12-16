using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingCommentingConfigurations
{
    [TestFixture]
    public class CreateMeetingCommentingConfigurationTests : TestBase
    {
        [Test]
        public async Task CreateMeetingCommentingCofiguration_WhenDataIsValid_IsSuccessful()
        {
            // Act
            var meetingId = await MeetingHelper.CreateMeetingAsync(MeetingsModule, ExecutionContext);

            // Assert
            var meetingCommentingConfiguration = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentingConfigurationQuery(meetingId));
            Assert.That(meetingCommentingConfiguration, Is.Not.Null);
        }
    }
}
