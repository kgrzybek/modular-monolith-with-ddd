using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.SeedWork;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    public class MeetingTestsBase : TestBase
    {
        protected class MeetingTestDataOptions
        {
            internal MemberId CreatorId { get; set; }

            internal MeetingTerm MeetingTerm { get; set; }

            internal Term RvspTerm { get; set; }

            internal int GuestsLimit { get; set; }

            internal int? AttendeesLimit { get; set; }

            internal bool IsMeetingCommentingEnabled { get; set; } = true;

            internal IEnumerable<MemberId> Attendees { get; set; } = Enumerable.Empty<MemberId>();
        }

        protected class MeetingTestData
        {
            public MeetingTestData(MeetingGroup meetingGroup, Meeting meeting, MeetingCommentingConfiguration meetingCommentingConfiguration)
            {
                MeetingGroup = meetingGroup;
                Meeting = meeting;
                MeetingCommentingConfiguration = meetingCommentingConfiguration;
            }

            internal MeetingGroup MeetingGroup { get; }

            internal Meeting Meeting { get; }

            internal MeetingCommentingConfiguration MeetingCommentingConfiguration { get; }
        }

        protected MeetingTestData CreateMeetingTestData(MeetingTestDataOptions options)
        {
            var proposalMemberId = options.CreatorId ?? new MemberId(Guid.NewGuid());
            var meetingProposal = MeetingGroupProposal.ProposeNew(
                "name",
                "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"),
                proposalMemberId);

            meetingProposal.Accept();

            var meetingGroup = meetingProposal.CreateMeetingGroup();

            meetingGroup.SetExpirationDate(DateTime.Now.AddDays(1));

            var meetingTerm = options.MeetingTerm ??
                              MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            var rsvpTerm = options.RvspTerm ?? Term.NoTerm;
            var meeting = meetingGroup.CreateMeeting(
                "title",
                meetingTerm,
                "description",
                MeetingLocation.CreateNew("Name", "Address", "PostalCode", "City"),
                options.AttendeesLimit,
                options.GuestsLimit,
                rsvpTerm,
                MoneyValue.Undefined,
                new List<MemberId>(),
                proposalMemberId);

            foreach (var attendee in options.Attendees)
            {
                meetingGroup.JoinToGroupMember(attendee);
                meeting.AddAttendee(meetingGroup, attendee, 0);
            }

            var meetingCommentingConfiguration = meeting.CreateCommentingConfiguration();
            if (options.IsMeetingCommentingEnabled)
            {
                meetingCommentingConfiguration.EnableCommenting(proposalMemberId, meetingGroup);
            }
            else
            {
                meetingCommentingConfiguration.DisableCommenting(proposalMemberId, meetingGroup);
            }

            DomainEventsTestHelper.ClearAllDomainEvents(meetingGroup);

            return new MeetingTestData(meetingGroup, meeting, meetingCommentingConfiguration);
        }
    }
}