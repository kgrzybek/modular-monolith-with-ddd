using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using static CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings.MeetingHelper;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingComments
{
    [TestFixture]
    public class GetMeetingCommentsTests : TestBase
    {
        [Test]
        public async Task GetMeetingComments_Test()
        {
            // Arrange
            var meetingId = await CreateMeetingAsync(MeetingsModule, ExecutionContext);
            
            await MeetingsModule.ExecuteCommandAsync(
                new AddMeetingCommentCommand(meetingId, "The meeting was great."));
            await MeetingsModule.ExecuteCommandAsync(
                new AddMeetingCommentCommand(meetingId, "The meeting was wonderful."));
            await MeetingsModule.ExecuteCommandAsync(
                new AddMeetingCommentCommand(meetingId, "The meeting was amazing."));
            
            // Act
            var meetingComments = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentsQuery(meetingId));
            
            // Assert
            Assert.That(meetingComments.Count, Is.EqualTo(3));
        }
    }
}