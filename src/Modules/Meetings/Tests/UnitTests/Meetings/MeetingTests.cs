using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingTests : MeetingTestsBase
    {
        [Test]
        public void CancelMeeting_WhenMeetingHasStarted_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.Cancel(creatorId);
            }); 
        }

        [Test]
        public void CancelMeeting_WhenMeetingHasNotStarted_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var date = DateTime.UtcNow;
            SystemClock.Set(date);

            meetingTestData.Meeting.Cancel(creatorId);

            var meetingCanceled = AssertPublishedDomainEvent<MeetingCanceledDomainEvent>(meetingTestData.Meeting);
            Assert.That(meetingCanceled.MeetingId, Is.EqualTo(meetingTestData.Meeting.Id));
            Assert.That(meetingCanceled.CancelMemberId, Is.EqualTo(creatorId));
            Assert.That(meetingCanceled.CancelDate, Is.EqualTo(date));           
        }

        [Test]
        public void RemoveAttendee_WhenMeetingHasStarted_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.RemoveAttendee(new MemberId(Guid.NewGuid()), creatorId, null);
            }); 
        }

        [Test]
        public void RemoveAttendee_WhenMemberIsNotAttendee_BreaksOnlyActiveAttendeeCanBeRemovedFromMeetingRule()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var attendeeToRemoveId = new MemberId(Guid.NewGuid());

            AssertBrokenRule<OnlyActiveAttendeeCanBeRemovedFromMeetingRule>(() =>
            {
                meetingTestData.Meeting.RemoveAttendee(attendeeToRemoveId, creatorId, null);
            }); 
        }

        [Test]
        public void RemoveAttendee_WhenMemberIsAttendee_AndReasonIsProvided_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var attendeeToRemoveId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(attendeeToRemoveId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, attendeeToRemoveId, 0);
            const string reason = "reasonOfRemoval";

            meetingTestData.Meeting.RemoveAttendee(attendeeToRemoveId, creatorId, reason);

            var meetingAttendeeRemoved = AssertPublishedDomainEvent<MeetingAttendeeRemovedDomainEvent>(meetingTestData.Meeting);
            Assert.That(meetingAttendeeRemoved.MemberId, Is.EqualTo(attendeeToRemoveId));
            Assert.That(meetingAttendeeRemoved.MeetingId, Is.EqualTo(meetingTestData.Meeting.Id));
            Assert.That(meetingAttendeeRemoved.Reason, Is.EqualTo(reason));
        }

        [Test]
        public void RemoveAttendee_WhenMemberIsAttendee_AndReasonIsNotProvided_BreaksReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var attendeeToRemoveId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(attendeeToRemoveId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, attendeeToRemoveId, 0);

            AssertBrokenRule<ReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule>(() =>
            {
                meetingTestData.Meeting.RemoveAttendee(attendeeToRemoveId, creatorId, null);
            });
        }
    }
}