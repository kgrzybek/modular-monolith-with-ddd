using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Administration.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Administration.IntegrationTests.MeetingGroupProposals
{
    [TestFixture]
    public class MeetingGroupProposalTests : TestBase
    {
        [Test]
        public async Task RequestMeetingGroupProposalVerification_Test()
        {
            var proposalId = await AdministrationModule.ExecuteCommandAsync(new RequestMeetingGroupProposalVerificationCommand(
                MeetingGroupProposalSampleData.MeetingGroupProposalId,
                MeetingGroupProposalSampleData.MeetingGroupProposalId,
                MeetingGroupProposalSampleData.Name,
                MeetingGroupProposalSampleData.Description,
                MeetingGroupProposalSampleData.LocationCity,
                MeetingGroupProposalSampleData.LocationCountryCode,
                MeetingGroupProposalSampleData.ProposalUserId,
                MeetingGroupProposalSampleData.ProposalDate));

            var meetingGroupProposal = await AdministrationModule.ExecuteQueryAsync(new GetMeetingGroupProposalQuery(proposalId));

            Assert.That(meetingGroupProposal.Id, Is.EqualTo(MeetingGroupProposalSampleData.MeetingGroupProposalId));
            Assert.That(meetingGroupProposal.StatusCode, Is.EqualTo(MeetingGroupProposalStatus.ToVerify.Value));
            Assert.That(meetingGroupProposal.Name, Is.EqualTo(MeetingGroupProposalSampleData.Name));
            Assert.That(meetingGroupProposal.Description, Is.EqualTo(MeetingGroupProposalSampleData.Description));
            Assert.That(meetingGroupProposal.LocationCity, Is.EqualTo(MeetingGroupProposalSampleData.LocationCity));
            Assert.That(meetingGroupProposal.LocationCountryCode, Is.EqualTo(MeetingGroupProposalSampleData.LocationCountryCode));
            Assert.That(meetingGroupProposal.ProposalUserId, Is.EqualTo(MeetingGroupProposalSampleData.ProposalUserId));
            Assert.That(meetingGroupProposal.ProposalDate, Is.EqualTo(MeetingGroupProposalSampleData.ProposalDate));
        }

        [Test]
        public async Task AcceptMeetingGroupProposal_WhenProposalIsNotAccepted_IsSuccessful()
        {
            var proposalId = await AdministrationModule.ExecuteCommandAsync(new RequestMeetingGroupProposalVerificationCommand(
                MeetingGroupProposalSampleData.MeetingGroupProposalId,
                MeetingGroupProposalSampleData.MeetingGroupProposalId,
                MeetingGroupProposalSampleData.Name,
                MeetingGroupProposalSampleData.Description,
                MeetingGroupProposalSampleData.LocationCity,
                MeetingGroupProposalSampleData.LocationCountryCode,
                MeetingGroupProposalSampleData.ProposalUserId,
                MeetingGroupProposalSampleData.ProposalDate));

            await AdministrationModule.ExecuteCommandAsync(
                new AcceptMeetingGroupProposalCommand(MeetingGroupProposalSampleData.MeetingGroupProposalId));

            var meetingGroupProposal = await AdministrationModule.ExecuteQueryAsync(new GetMeetingGroupProposalQuery(proposalId));
            Assert.That(meetingGroupProposal.StatusCode, Is.EqualTo(MeetingGroupProposalStatus.Verified.Value));
            Assert.That(meetingGroupProposal.DecisionUserId, Is.EqualTo(ExecutionContext.UserId));

            var acceptedNotification = await GetLastOutboxMessage<MeetingGroupProposalAcceptedNotification>();

            Assert.That(acceptedNotification.DomainEvent.MeetingGroupProposalId.Value, Is.EqualTo(proposalId));
        }

        [Test]
        public async Task AcceptMeetingGroupProposal_WhenProposalIsAlreadyAccepted_BreaksMeetingGroupProposalCanBeVerifiedOnceRule()
        {
            var proposalId = await AdministrationModule.ExecuteCommandAsync(new RequestMeetingGroupProposalVerificationCommand(
                MeetingGroupProposalSampleData.MeetingGroupProposalId,
                MeetingGroupProposalSampleData.MeetingGroupProposalId,
                MeetingGroupProposalSampleData.Name,
                MeetingGroupProposalSampleData.Description,
                MeetingGroupProposalSampleData.LocationCity,
                MeetingGroupProposalSampleData.LocationCountryCode,
                MeetingGroupProposalSampleData.ProposalUserId,
                MeetingGroupProposalSampleData.ProposalDate));

            await AdministrationModule.ExecuteCommandAsync(
                new AcceptMeetingGroupProposalCommand(MeetingGroupProposalSampleData.MeetingGroupProposalId));

            AssertBrokenRule<MeetingGroupProposalCanBeVerifiedOnceRule>(async () =>
                await AdministrationModule.ExecuteCommandAsync(
                    new AcceptMeetingGroupProposalCommand(MeetingGroupProposalSampleData.MeetingGroupProposalId)));
        }
    }
}