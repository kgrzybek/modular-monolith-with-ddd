using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Administration.Domain.UnitTests.SeedWork;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.UnitTests.MeetingGroupProposals
{
    [TestFixture]
    public class MeetingGroupProposalTests : TestBase
    {
        [Test]
        public void CreateProposalToVerify_IsSuccessful()
        {
            var meetingGroupProposalId = Guid.NewGuid();
            var location = MeetingGroupLocation.Create("Warsaw", "Poland");
            var proposalUserId = new UserId(Guid.NewGuid());
            var proposalDate = DateTime.Now;

            var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
                meetingGroupProposalId,
                "meetingName",
                "meetingDescription",
                location,
                proposalUserId,
                proposalDate);

            var meetingGroupProposalVerificationRequested = AssertPublishedDomainEvent<MeetingGroupProposalVerificationRequestedDomainEvent>(meetingGroupProposal);
            Assert.That(meetingGroupProposalVerificationRequested.MeetingGroupProposalId, Is.EqualTo(new MeetingGroupProposalId(meetingGroupProposalId)));
        }

        [Test]
        public void AcceptProposal_WhenDecisionIsNotMade_IsSuccessful()
        {
            var meetingGroupProposalId = Guid.NewGuid();
            var location = MeetingGroupLocation.Create("Warsaw", "Poland");
            var proposalUserId = new UserId(Guid.NewGuid());
            var proposalDate = DateTime.Now;
            var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
                meetingGroupProposalId,
                "meetingName",
                "meetingDescription",
                location,
                proposalUserId,
                proposalDate);

            meetingGroupProposal.Accept(new UserId(Guid.NewGuid()));

            var meetingGroupProposalAccepted = AssertPublishedDomainEvent<MeetingGroupProposalAcceptedDomainEvent>(meetingGroupProposal);

            Assert.That(meetingGroupProposalAccepted.MeetingGroupProposalId, Is.EqualTo(new MeetingGroupProposalId(meetingGroupProposalId)));
        }

        [Test]
        public void AcceptProposal_WhenDecisionIsMade_CanBeVerifiedOnlyOnce()
        {
            var meetingGroupProposalId = Guid.NewGuid();
            var location = MeetingGroupLocation.Create("Warsaw", "Poland");
            var userId = new UserId(Guid.NewGuid());
            var proposalUserId = userId;
            var proposalDate = DateTime.Now;
            var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
                meetingGroupProposalId,
                "meetingName",
                "meetingDescription",
                location,
                proposalUserId,
                proposalDate);

            meetingGroupProposal.Accept(userId);

            AssertBrokenRule<MeetingGroupProposalCanBeVerifiedOnceRule>(() =>
            {
                meetingGroupProposal.Accept(userId);
            });
        }

        [Test]
        public void RejectProposal_WhenDecisionIsMade_CanBeVerifiedOnlyOnce()
        {
            var meetingGroupProposalId = Guid.NewGuid();
            var location = MeetingGroupLocation.Create("Warsaw", "Poland");
            var userId = new UserId(Guid.NewGuid());
            var proposalUserId = userId;
            var proposalDate = DateTime.Now;
            var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
                meetingGroupProposalId,
                "meetingName",
                "meetingDescription",
                location,
                proposalUserId,
                proposalDate);

            meetingGroupProposal.Accept(userId);

            AssertBrokenRule<MeetingGroupProposalCanBeVerifiedOnceRule>(() =>
            {
                meetingGroupProposal.Reject(userId, "rejectReason");
            });
        }

        [Test]
        public void RejectProposal_WithoutProvidedReason_CannotBeRejected()
        {
            var meetingGroupProposalId = Guid.NewGuid();
            var location = MeetingGroupLocation.Create("Warsaw", "Poland");
            var userId = new UserId(Guid.NewGuid());
            var proposalUserId = userId;
            var proposalDate = DateTime.Now;
            var meetingGroupProposal = MeetingGroupProposal.CreateToVerify(
                meetingGroupProposalId,
                "meetingName",
                "meetingDescription",
                location,
                proposalUserId,
                proposalDate);

            AssertBrokenRule<MeetingGroupProposalRejectionMustHaveAReasonRule>(() =>
            {
                meetingGroupProposal.Reject(userId, string.Empty);
            });
        }
    }
}