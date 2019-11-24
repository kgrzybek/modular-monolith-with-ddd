using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingAddAttendeeTests : MeetingTestsBase
    {
        [Test]
        public void AddAttendee_WhenMeetingHasStared_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });         

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, creatorId, 0);
            });          
        }

        [Test]
        public void AddAttendee_WhenRsvpTermEnded_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                RvspTerm = Term.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });         

            AssertBrokenRule<AttendeeCanBeAddedOnlyInRsvpTermRule>(() =>
            {
                meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, creatorId, 0);
            });          
        }

        [Test]
        public void AddAttendee_WhenAttendeeIsNotAMemberOfMeetingGroup_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });         

            AssertBrokenRule<MeetingAttendeeMustBeAMemberOfGroupRule>(() =>
            {
                meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, new MemberId(Guid.NewGuid()), 0);
            });          
        }

        [Test]
        public void AddAttendee_WhenMemberIsAlreadyAttendeeOfMeeting_IsNotPossible()
        {
            // Arrange
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });    
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 0);

            // Assert
            AssertBrokenRule<MemberCannotBeAnAttendeeOfMeetingMoreThanOnceRule>(() =>
            {
                // Act
                meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 0);
            });          
        }

        [Test]
        public void AddAttendee_WhenGuestsNumberIsAboveTheLimit_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                GuestsLimit = 5
            });    
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);           

            AssertBrokenRule<MeetingGuestsNumberIsAboveLimitRule>(() =>
            {
                meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 6);
            });          
        }

        [Test]
        public void AddAttendee_WhenAttendeeLimitIsReached_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                AttendeesLimit = 5
            });    
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);   
            var aboveLimitMember = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(aboveLimitMember);  

            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 3);

            // for now: creator or meeting (automatically) and newMember with 3 guests = 5
            AssertBrokenRule<MeetingAttendeesNumberIsAboveLimitRule>(() =>
            {
                meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, aboveLimitMember, 0);
            });          
        }

        [Test]
        public void AddAttendee_WhenAllConditionsAllowsNewAttendee_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });    
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);

            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 3);

            var meetingAttendeesAddedEvents =
                AssertPublishedDomainEvents<MeetingAttendeeAddedDomainEvent>(meetingTestData.Meeting);
            Assert.That(meetingAttendeesAddedEvents.Count, Is.EqualTo(2));
            Assert.That(meetingAttendeesAddedEvents[0].AttendeeId, Is.EqualTo(creatorId));
            Assert.That(meetingAttendeesAddedEvents[0].Role, Is.EqualTo(MeetingAttendeeRole.Host));
            Assert.That(meetingAttendeesAddedEvents[1].AttendeeId, Is.EqualTo(newMemberId));
            Assert.That(meetingAttendeesAddedEvents[1].Role, Is.EqualTo(MeetingAttendeeRole.Attendee));
        }

        [Test]
        public void AddAttendee_WhenMemberIsNotAttendeeAndChangedDecision_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });    
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);
            meetingTestData.Meeting.AddNotAttendee(newMemberId);
            
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 0);

            var meetingNotAttendeeChangedDecision = AssertPublishedDomainEvent<MeetingNotAttendeeChangedDecisionDomainEvent>(meetingTestData.Meeting);
            Assert.That(meetingNotAttendeeChangedDecision.MemberId, Is.EqualTo(newMemberId));
        }
    }
}