using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.DisableMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.EnableMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingCommentingConfigurations
{
    [TestFixture]
    public class EnableMeetingCommentingConfigurationTests : TestBase
    {
        [Test]
        public async Task EnableMeetingCommenting_WhenDataIsValid_IsSuccess()
        {
            // Arrange
            var meetingId = await MeetingHelper.CreateMeetingAsync(MeetingsModule, ExecutionContext);
            await MeetingsModule.ExecuteCommandAsync(new DisableMeetingCommentingConfigurationCommand(meetingId));

            // Act
            await MeetingsModule.ExecuteCommandAsync(new EnableMeetingCommentingConfigurationCommand(meetingId));

            // Assert
            var meetingConfiguration = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentingConfigurationQuery(meetingId));
            Assert.That(meetingConfiguration, Is.Not.Null);
            Assert.That(meetingConfiguration.IsCommentingEnabled, Is.True);
        }

        [Test]
        public void EnableMeetingCommenting_WhenConfigurationNotExist_ThrowsInvalidCommandException()
        {
            // Assert
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                // Act
                await MeetingsModule.ExecuteCommandAsync(new EnableMeetingCommentingConfigurationCommand(Guid.NewGuid()));
            });
        }
    }
}