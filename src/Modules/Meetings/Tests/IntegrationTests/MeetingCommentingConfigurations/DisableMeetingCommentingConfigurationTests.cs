using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.DisableMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingCommentingConfigurations
{
    [TestFixture]
    public class DisableMeetingCommentingConfigurationTests : TestBase
    {
        [Test]
        public async Task DisableMeetingCommenting_WhenDataIsValid_IsSuccess()
        {
            // Arrange
            var meetingId = await MeetingHelper.CreateMeetingAsync(MeetingsModule, ExecutionContext);

            // Act
            await MeetingsModule.ExecuteCommandAsync(new DisableMeetingCommentingConfigurationCommand(meetingId));

            // Assert
            var meetingConfiguration = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentingConfigurationQuery(meetingId));
            Assert.That(meetingConfiguration, Is.Not.Null);
            Assert.That(meetingConfiguration.IsCommentingEnabled, Is.False);
        }

        [Test]
        public void DisableMeetingCommenting_WhenConfigurationNotExist_ThrowsInvalidCommandException()
        {
            // Assert
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                // Act
                await MeetingsModule.ExecuteCommandAsync(new DisableMeetingCommentingConfigurationCommand(Guid.NewGuid()));
            });
        }
    }
}