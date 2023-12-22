namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingGroupProposals
{
    public struct MeetingGroupProposalSampleData
    {
        public static Guid MeetingGroupProposalId = Guid.NewGuid();

        public static string Name = "Great Meeting";

        public static string Description = "Great Meeting description";

        public static string LocationCity = "Warsaw";

        public static string LocationCountryCode = "PL";

        public static Guid ProposalUserId = Guid.NewGuid();

        public static DateTime ProposalDate = new DateTime(2020, 1, 1, 10, 20, 00);
    }
}