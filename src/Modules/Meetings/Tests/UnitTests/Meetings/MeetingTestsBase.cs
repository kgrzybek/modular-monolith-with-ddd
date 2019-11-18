using System;
using System.Collections.Generic;
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

            internal int GuestsLimit{ get; set; }

            internal int? AttendeesLimit { get; set; }
        }

        protected class MeetingTestData
        {
            public MeetingTestData(MeetingGroup meetingGroup, Meeting meeting)
            {
                MeetingGroup = meetingGroup;
                Meeting = meeting;
            }

            internal MeetingGroup MeetingGroup { get; }

            internal Meeting Meeting { get; }
        }

        protected MeetingTestData CreateMeetingTestData(MeetingTestDataOptions options)
        {
            var proposalMemberId = options.CreatorId ?? new MemberId(Guid.NewGuid());
            var meetingProposal = MeetingGroupProposal.ProposeNew(
                "name", "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"), proposalMemberId);

            meetingProposal.Accept();

            var meetingGroup = meetingProposal.CreateMeetingGroup();

            meetingGroup.UpdatePaymentInfo(DateTime.Now.AddDays(1));

            var meetingTerm = options.MeetingTerm ??
                              MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            var rsvpTerm = options.RvspTerm ?? Term.NoTerm;
            var meeting = meetingGroup.CreateMeeting("title",
                meetingTerm,
                "description",
                MeetingLocation.CreateNew("Name", "Address", "PostalCode", "City"),
                options.AttendeesLimit,
                options.GuestsLimit,
                rsvpTerm,
                MoneyValue.Undefined,
                new List<MemberId>(),
                proposalMemberId);

            DomainEventsTestHelper.ClearAllDomainEvents(meetingGroup);

            return new MeetingTestData(meetingGroup, meeting);
        }
    }
}