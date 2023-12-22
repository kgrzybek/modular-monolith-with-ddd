using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetAllMeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingGroupProposals
{
    [TestFixture]
    public class GetMeetingGroupProposalsTests : TestBase
    {
        [Test]
        public async Task GetMeetingGroupProposals_Test()
        {
            await MeetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                "Name 1",
                "Desc 1",
                "Warsaw",
                "PL"));

            await MeetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                "Name 2",
                "Desc 2",
                "London",
                "GB"));

            await MeetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                "Name 3",
                "Desc 3",
                "Rome",
                "IT"));

            await MeetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                "Name 4",
                "Desc 4",
                "Madrid",
                "ES"));

            await MeetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                "Name 5",
                "Desc 5",
                "Berlin",
                "DE"));

            var allProposals = await MeetingsModule.ExecuteQueryAsync(new GetAllMeetingGroupProposalsQuery(null, null));

            Assert.That(allProposals.Count, Is.EqualTo(5));

            var proposalsPaged = await MeetingsModule.ExecuteQueryAsync(new GetAllMeetingGroupProposalsQuery(2, 2));

            Assert.That(proposalsPaged.Count, Is.EqualTo(2));
            Assert.That(proposalsPaged[0].Name, Is.EqualTo("Name 3"));
            Assert.That(proposalsPaged[1].Name, Is.EqualTo("Name 4"));
        }
    }
}