using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingGroupProposals
{
    [TestFixture]
    public class ProposeMeetingGroupTests : TestBase
    {
        [Test]
        public async Task ProposeAndAcceptMeetingGroup_WhenDataIsValid_IsSuccessful()
        {
            var proposalId = await MeetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                MeetingGroupProposalSampleData.Name,
                MeetingGroupProposalSampleData.Description,
                MeetingGroupProposalSampleData.LocationCity,
                MeetingGroupProposalSampleData.LocationCountryCode));

            await MeetingsModule.ExecuteCommandAsync(new AcceptMeetingGroupProposalCommand(Guid.NewGuid(), proposalId));

            var meetingGroupProposal =
                await MeetingsModule.ExecuteQueryAsync(new GetMeetingGroupProposalQuery(proposalId));

            Assert.That(meetingGroupProposal.Id, Is.EqualTo(proposalId));
            Assert.That(meetingGroupProposal.Name, Is.EqualTo(MeetingGroupProposalSampleData.Name));
            Assert.That(meetingGroupProposal.Description, Is.EqualTo(MeetingGroupProposalSampleData.Description));
            Assert.That(meetingGroupProposal.LocationCity, Is.EqualTo(MeetingGroupProposalSampleData.LocationCity));
            Assert.That(meetingGroupProposal.LocationCountryCode, Is.EqualTo(MeetingGroupProposalSampleData.LocationCountryCode));
            Assert.That(meetingGroupProposal.StatusCode, Is.EqualTo("Accepted"));
        }

        [Test]
        public void ProposeMeetingGroup_WhenNoLocationProvided_ThrowsInvalidCommandException()
        {
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await MeetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                    MeetingGroupProposalSampleData.Name,
                    MeetingGroupProposalSampleData.Description,
                    null,
                    null));
            });
        }
    }
}