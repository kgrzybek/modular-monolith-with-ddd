using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups
{
    public class MeetingGroupLocation : ValueObject
    {
        public static MeetingGroupLocation CreateNew(string city, string countryCode)
        {
            return new MeetingGroupLocation(city, countryCode);
        }

        public string City { get; }

        public string CountryCode { get; }

        private MeetingGroupLocation(string city, string countryCode)
        {
            City = city;
            CountryCode = countryCode;
        }
    }
}