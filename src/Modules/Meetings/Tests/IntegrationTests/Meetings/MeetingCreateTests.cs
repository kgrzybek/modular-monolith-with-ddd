using CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingAttendees;
using CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingDetails;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings
{
    [TestFixture]
    public class MeetingCreateTests : TestBase
    {
        [Test]
        public async Task CreateMeeting_Test()
        {
            // Arrange
            await MeetingsModule.ExecuteCommandAsync(new CreateMemberCommand(
                Guid.NewGuid(),
                ExecutionContext.UserId,
                "johndoe",
                "johndoe@mail.com",
                "John",
                "Doe",
                "John Doe"));

            // Act
            var meetingId = await MeetingHelper.CreateMeetingAsync(
                MeetingsModule,
                ExecutionContext);

            // Assert
            var meetingDetails = await MeetingsModule.ExecuteQueryAsync(new GetMeetingDetailsQuery(meetingId));
            Assert.That(meetingDetails, Is.Not.Null);

            var meetingAttendees =
                await MeetingsModule.ExecuteQueryAsync(new GetMeetingAttendeesQuery(meetingId));

            Assert.That(meetingAttendees.Count, Is.EqualTo(1));
        }
    }
}