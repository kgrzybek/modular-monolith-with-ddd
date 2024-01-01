using CompanyName.MyMeetings.SUT.Helpers;
using CompanyName.MyMeetings.SUT.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.SUT.TestCases
{
    public class CreateMeeting : TestBase
    {
        protected override bool PerformDatabaseCleanup => true;

        [Test]
        public async Task Prepare()
        {
            await UsersFactory.GivenAdmin(
                UserAccessModule,
                "testAdmin@mail.com",
                "testAdminPass",
                "Jane Doe",
                "Jane",
                "Doe",
                "testAdmin@mail.com");

            var userId = await UsersFactory.GivenUser(
                RegistrationsModule,
                ConnectionString,
                "adamSmith@mail.com",
                "adamSmithPass",
                "Adam",
                "Smith",
                "adamSmith@mail.com");

            ExecutionContextAccessor.SetUserId(userId);

            var meetingGroupId = await MeetingGroupsFactory.GivenMeetingGroup(
                MeetingsModule,
                AdministrationModule,
                ConnectionString,
                "Software Craft",
                "Group for software craft passionates",
                "Warsaw",
                "PL");

            await TestPriceListManager.AddPriceListItems(PaymentsModule, ConnectionString);

            await TestPaymentsManager.BuySubscription(
                PaymentsModule,
                ExecutionContextAccessor);

            SetDate(new DateTime(2022, 7, 1, 10, 0, 0));

            var meetingId = await TestMeetingFactory.GivenMeeting(
                MeetingsModule,
                meetingGroupId,
                "Tactical DDD",
                new DateTime(2022, 7, 10, 18, 0, 0),
                new DateTime(2022, 7, 10, 20, 0, 0),
                "Meeting about Tactical DDD patterns",
                "Location Name",
                "Location Address",
                "01-755",
                "Warsaw",
                50,
                0,
                null,
                null,
                0,
                null,
                []);

            var attendeeUserId = await UsersFactory.GivenUser(
                RegistrationsModule,
                ConnectionString,
                "rickmorty@mail.com",
                "rickmortyPass",
                "Rick",
                "Morty",
                "rickmorty@mail.com");

            ExecutionContextAccessor.SetUserId(attendeeUserId);

            await TestMeetingGroupManager.JoinToGroup(MeetingsModule, meetingGroupId);

            await TestMeetingManager.AddAttendee(MeetingsModule, meetingId, guestsNumber: 1);
        }
    }
}